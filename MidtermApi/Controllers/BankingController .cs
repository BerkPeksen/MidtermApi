using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using MidtermApi.Data;
using MidtermApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MidtermApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    public class BankingController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BankingController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("BankQueryTuition/{studentNo}")]
        [Authorize]
        public ActionResult<TuitionQueryResponse> QueryTuition(string studentNo)
        {
            var student = _context.Students.FirstOrDefault(s => s.StudentNo == studentNo);
            if (student == null)
            {
                return NotFound("Student not found");
            }

            return Ok(new TuitionQueryResponse { TuitionTotal = student.TuitionTotal, Balance = student.Balance });
        }

        [HttpPost("AddBalance")]
        public ActionResult<TuitionResponse> AddTuition(string studentNo,int amount)
        {
            var student = _context.Students.FirstOrDefault(s => s.StudentNo == studentNo);
            if (student == null)
            {
                return NotFound("Student not found");
            }


            student.Balance += amount;

            _context.SaveChanges();

            return Ok(new TuitionResponse { Status = "Success" });
        }

        [HttpPost("SendPaymentMessage")]
        public ActionResult SendPaymentMessage(string studentNo, string term)
        {
            // Create a connection to RabbitMQ
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                // Declare a queue named "payment_queue"
                channel.QueueDeclare(queue: "payment_queue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                // Prepare payment details
                var paymentDetails = $"{studentNo},{term}";

                // Convert payment details to bytes
                var body = Encoding.UTF8.GetBytes(paymentDetails);

                // Publish the payment details to the "payment_queue"
                channel.BasicPublish(exchange: "",
                                     routingKey: "payment_queue",
                                     basicProperties: null,
                                     body: body);
            }

            return Ok("Payment request sent");
        }
        [HttpPost("ConsumeMessage")]
        public  ActionResult ConsumeMessage()
        {
            string paymentDetails = "";

            // Create a connection to RabbitMQ
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                // Declare the queue
                channel.QueueDeclare(queue: "payment_queue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                // Create a consumer
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    // Get the payment details from the message
                    var body = ea.Body.ToArray();
                    paymentDetails = Encoding.UTF8.GetString(body);

                    // Process the payment
                    Console.WriteLine($"Processing payment for: {paymentDetails}");

                    // Acknowledge the message
                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                };

                // Start consuming messages
                channel.BasicConsume(queue: "payment_queue",
                                     autoAck: false,
                                     consumer: consumer);
                while (string.IsNullOrEmpty(paymentDetails))
                {
                    Thread.Sleep(100); // Sleep for a short interval to avoid busy-waiting
                }


            }
            var details = paymentDetails.Split(',');
            string studentNo = details[0];
            string term = details[1];
            var student = _context.Students.FirstOrDefault(s => s.StudentNo == studentNo && s.Term == term);
            if (student == null)
            {
                return NotFound("Student not found");
            }

            if (student.Balance > 0 && student.TuitionTotal > 0)
            {
                if (student.Balance > student.TuitionTotal)
                {
                    student.Balance -= student.TuitionTotal;
                    student.TuitionTotal = 0;
                }
                else
                {
                    student.TuitionTotal -= student.Balance;
                    student.Balance = 0;
                }
                _context.SaveChanges();

                return Ok(new TuitionResponse { Status = "Successful" });
            }
            else
            {
                if (student.TuitionTotal == 0)
                {
                    return BadRequest("Tuition is already paid");
                }
                else
                {
                    return BadRequest("Insufficient balance to pay tuition");
                }
            }
        }

    
    }
}
