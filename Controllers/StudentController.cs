using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using school.Models;

namespace school.Controllers
{
    [Route("student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpPost]
        public ActionResult<Student> Post(CreateStudentDto createStudentDto)
        {
            var student = new Student
            {
                Id = Guid.NewGuid(),
                Name = createStudentDto.Name,
                Age = createStudentDto.Age,
                Email = createStudentDto.Email,
                CreatedTime = DateTime.Now
            };
            if (student != null)
            {
                using (var context = new LibraryContext())
                {
                    context.Students.Add(student);
                    context.SaveChanges();
                    return StatusCode(201, student);
                }

            }return BadRequest();
        }

        [HttpGet]
        public ActionResult<Student> Get()
        {
            using (var context = new LibraryContext())
            {
                return Ok(context.Students.ToList());
            }
        }

        [HttpGet("/withMark")]
        public ActionResult<Student> GetWithMark()
        {
            using (var context = new LibraryContext())
            {
                return Ok(context.Students.Include(x => x.Mark).ToList());
            }
        }

        [HttpGet("/WithId")]
        public ActionResult<Student> GetWithId(string id)
        {
            if (id != "")
            {
                using (var context = new LibraryContext())
                {
                    return Ok(context.Students.ToList());
                }
            }
            return BadRequest();
        }

        [HttpDelete]
        public ActionResult<Student> Delete(Guid id)
        {
            using (var context = new LibraryContext())
            {
                var delStudent = context.Students.FirstOrDefault(y => y.Id == id);
                if (delStudent != null)
                {
                    context.Students.Remove(delStudent);
                    context.SaveChanges();
                    return StatusCode(200, delStudent);
                }
                return NotFound();
            }
        }

        [HttpPut]
        public ActionResult<Student> Put(Guid id, UpdateStudentDto updateStudentDto)
        {
            using (var context = new LibraryContext())
            {
                var existingStudent = context.Students.FirstOrDefault(x => x.Id == id);
                if (existingStudent != null)
                {
                    existingStudent.Name = updateStudentDto.Name;
                    existingStudent.Age = updateStudentDto.Age;
                    existingStudent.Email = updateStudentDto.Email;
                    context.SaveChanges();
                    return StatusCode(200, existingStudent);
                }
                return NotFound();
            }
        }
    }
}
