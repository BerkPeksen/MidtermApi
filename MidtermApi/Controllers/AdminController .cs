using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MidtermApi.Data;
using MidtermApi.Models;

namespace MidtermApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("add-tuition")]
        [Authorize]
        public ActionResult<AddTuitionResponse> AddTuition(string studentNo, string term, int amount)
        {
            var student = _context.Students.FirstOrDefault(s => s.StudentNo == studentNo && s.Term == term);
            if (student == null)
            {
                return NotFound("Student not found");
            }

            
            student.TuitionTotal += amount;

            _context.SaveChanges();

            return Ok(new AddTuitionResponse { TransactionStatus = "Success" });
        }

        [HttpGet("unpaid-tuition-status")]
        [Authorize]
        public ActionResult<UnpaidTuitionStatusResponse> UnpaidTuitionStatus(int page = 1, int pageSize = 10)
        {
            var unpaidStudents = _context.Students.Where(s => s.Status =="unpaid").ToList();

            var totalCount = unpaidStudents.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var pagedStudents = unpaidStudents.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var unpaidStudentDtos = pagedStudents.Select(s => new UnpaidTuitionStatus
            {
                StudentNo = s.StudentNo,
                Term = s.Term,
                UnpaidAmount = s.TuitionTotal
            }).ToList();

            return Ok(new UnpaidTuitionStatusResponse { Students = unpaidStudentDtos });
        }
    }
}
