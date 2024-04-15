using Microsoft.AspNetCore.Authorization;
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

        [HttpPost("PayTuition")]
        public ActionResult<TuitionResponse> PayTuition(string studentNo, string term)
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

                return Ok(new TuitionResponse { Status = "Successful" });
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
