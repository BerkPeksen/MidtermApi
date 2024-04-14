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
    public class MobileController : ControllerBase
    {
        private AppDbContext _context;

        public MobileController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("MobileQueryTuition/{studentNo}")]
        public ActionResult<TuitionQueryResponse> QueryTuition(string studentNo)
        {
            var student = _context.Students.FirstOrDefault(s => s.StudentNo == studentNo);
            if (student == null)
            {
                return NotFound("Student not found");
            }

            return Ok(new TuitionQueryResponse { TuitionTotal = student.TuitionTotal, Balance = student.Balance });
        }
    }
}
