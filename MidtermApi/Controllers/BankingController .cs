﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("BankQueryTuition/{studentNo}")]
        public ActionResult<TuitionQueryResponse> QueryTuition(string studentNo)
        {
            var student = _context.Students.FirstOrDefault(s => s.StudentNo == studentNo);
            if (student == null)
            {
                return NotFound("Student not found");
            }

            return Ok(new TuitionQueryResponse { TuitionTotal = student.TuitionTotal, Balance = student.Balance });
        }

        [HttpPost("add-balance")]
        public ActionResult<AddTuitionResponse> AddTuition(string studentNo,int amount)
        {
            var student = _context.Students.FirstOrDefault(s => s.StudentNo == studentNo);
            if (student == null)
            {
                return NotFound("Student not found");
            }


            student.Balance += amount;

            _context.SaveChanges();

            return Ok(new AddTuitionResponse { TransactionStatus = "Success" });
        }

        [HttpPost("pay-tuition")]
        public ActionResult<PayTuitionResponse> PayTuition(string studentNo, string term)
        {
            var student = _context.Students.FirstOrDefault(s => s.StudentNo == studentNo && s.Term == term);
            if (student == null)
            {
                return NotFound("Student not found");
            }

            if(student.Balance  > 0 && student.TuitionTotal > 0)
            { 
                if(student.Balance > student.TuitionTotal)
                {
                    student.Balance -= student.TuitionTotal;
                    student.TuitionTotal = 0;
                }
                else { 
                    student.TuitionTotal -= student.Balance;
                    student.Balance = 0;
                }
                _context.SaveChanges();

                return Ok(new PayTuitionResponse { PaymentStatus = "Successful" });
            }
            else
            {
                if(student.TuitionTotal == 0)
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
