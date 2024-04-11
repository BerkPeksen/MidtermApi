using Microsoft.AspNetCore.Mvc;
using MidtermApi.Data;
using MidtermApi.Models;
using System;
using System.Collections.Generic;

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

        [HttpGet("query-tuition")]
        public ActionResult<TuitionQueryResponse> QueryTuition(string studentNo)
        {
            var student = _context.Students.FirstOrDefault(s => s.StudentNo == studentNo);
            if (student == null)
            {
                return NotFound("Student not found");
            }

            return Ok(new TuitionQueryResponse { TuitionTotal = student.TuitionTotal, Balance = student.Balance });
        }

        [HttpPost("pay-tuition")]
        public ActionResult<PayTuitionResponse> PayTuition(PayTuitionRequest request)
        {
            var student = _context.Students.FirstOrDefault(s => s.StudentNo == request.StudentNo && s.Term == request.Term);
            if (student == null)
            {
                return NotFound("Student not found");
            }

            // Add logic to process payment
            // For example, update student's balance and payment status
            student.Balance -= student.TuitionTotal;
            student.Status = "Paid";

            _context.SaveChanges();

            return Ok(new PayTuitionResponse { PaymentStatus = "Successful" });
        }
    }
}
