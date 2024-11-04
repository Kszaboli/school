using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}
