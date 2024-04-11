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
        public ActionResult<AddTuitionResponse> AddTuition(AddTuitionRequest request)
        {
            var student = _context.Students.FirstOrDefault(s => s.StudentNo == request.StudentNo && s.Term == request.Term);
            if (student == null)
            {
                return NotFound("Student not found");
            }

            // Add logic to update student's tuition amount
            // For example, increment the tuition total by a fixed amount
            student.TuitionTotal += 1000;

            _context.SaveChanges();

            return Ok(new AddTuitionResponse { TransactionStatus = "Success" });
        }

        [HttpGet("unpaid-tuition-status")]
        public ActionResult<UnpaidTuitionStatusResponse> UnpaidTuitionStatus(int page = 1, int pageSize = 10)
        {
            var unpaidStudents = _context.Students.Where(s => s.Status =="unpaid").ToList();

            var totalCount = unpaidStudents.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var pagedStudents = unpaidStudents.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var unpaidStudentDtos = pagedStudents.Select(s => new UnpaidTuitionStatus
            {
                StudentNo = s.StudentNo,
                UnpaidAmount = s.Balance
            }).ToList();

            return Ok(new UnpaidTuitionStatusResponse { Students = unpaidStudentDtos });
        }
    }
}
