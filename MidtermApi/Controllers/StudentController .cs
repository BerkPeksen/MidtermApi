using Microsoft.AspNetCore.Mvc;
using MidtermApi.Data;
using MidtermApi.Models;
using System.Collections.Generic;

namespace MidtermApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<StudentDto>> GetStudents()
        {
            var students = _context.GetAllStudents();
            var studentDtos = students.Select(s => new StudentDto
            {
                Id = s.Id,
                StudentNo = s.StudentNo,
                Term = s.Term,
                TuitionTotal = s.TuitionTotal,
                Balance = s.Balance,
                Status = s.Status
            }).ToList();

            return Ok(studentDtos);
        }

        [HttpPost]
        public ActionResult AddStudent(StudentDto studentDto)
        {
            var student = new Student
            {
                StudentNo = studentDto.StudentNo,
                Term = studentDto.Term,
                TuitionTotal = studentDto.TuitionTotal,
                Balance = studentDto.Balance,
                Status = studentDto.Status
            };

            _context.AddStudent(student);

            return Ok();
        }
    }
}