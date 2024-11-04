using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using school.Models;

namespace school.Controllers
{
    [Route("mark")]
    [ApiController]
    public class MarkController : ControllerBase
    {
        [HttpPost]
        public ActionResult<Mark> Post(CreateMarkDto createMarkDto)
        {
            var mark = new Mark()
            {
                Id = Guid.NewGuid(),
                Marknumber = createMarkDto.Marknumber,
                Marktext = createMarkDto.Marktext,
                Description = createMarkDto.Description,
                CreatedTime = DateTime.Now,
                UpdatedTime = DateTime.Now,
                StudentId = createMarkDto?.StudentId
            };
            if (mark != null)
            {
                using (var context = new LibraryContext())
                {
                    context.Marks.Add(mark);
                    context.SaveChanges();
                    return StatusCode(201, mark);
                }
            }
            return BadRequest();
        }
        [HttpGet]
        public ActionResult<Mark> Get()
        {
            using (var context = new LibraryContext())
            {
                return Ok(context.Marks.ToList());
            }
        }

        [HttpGet("/WithId")]
        public ActionResult<Mark> GetWithId(string id)
        {
            if (id != "")
            {
                using (var context = new LibraryContext())
                {
                    return Ok(context.Marks.ToList());
                }
            }
            return BadRequest();
        }

        [HttpDelete]
        public ActionResult<Mark> Delete(Guid id)
        {
            using (var context = new LibraryContext())
            {
                var delMark = context.Marks.FirstOrDefault(y => y.Id == id);
                if (delMark != null)
                {
                    context.Marks.Remove(delMark);
                    context.SaveChanges();
                    return StatusCode(200, delMark);
                }
                return NotFound();
            }
        }

        [HttpPut]
        public ActionResult<Mark> Put(Guid id, UpdateMarkDto updateMarkDto)
        {
            using (var context = new LibraryContext())
            {
                var existingMark = context.Marks.FirstOrDefault(x => x.Id == id);
                if (existingMark != null)
                {
                    existingMark.Marknumber = updateMarkDto.Marknumber;
                    existingMark.Marktext = updateMarkDto.Marktext;
                    existingMark.Description = updateMarkDto.Description;
                    existingMark.UpdatedTime = DateTime.Now;
                    context.SaveChanges();
                    return StatusCode(200, existingMark);
                }
                return NotFound();
            }
        }
    }
}
