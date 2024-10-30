using FinalProject.DTO;
using FinalProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialController : ControllerBase
    {
        private readonly MyDbContext _db;

        public TestimonialController(MyDbContext db)
        {
            _db = db;
        }


        [HttpPost("AddTestimonial/{id}")]
        public IActionResult Addtestimonial(int id, [FromBody] AddTestimonial addtestimonialDTO)
        {
            if (id == null || id == 0)
            {
                return BadRequest("The id is null or 0 here");
            }

            var user = _db.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var addtestimonial = new Testimonial
            {
                UserId = id,
                TestimonialMessege = addtestimonialDTO.TheTestimonial,
                IsAccept = false,


            };
            _db.Testimonials.Add(addtestimonial);
            _db.SaveChanges();
            return Ok();
        }


        [HttpGet("GetAllAcceptedTestimonial")]
        public IActionResult GetAllAcceptedTestimonial()
        {
            var testimonials = _db.Testimonials
            .Include(c => c.User)
            .Where(d => d.IsAccept == true)
            .Select(c => new {
                TestimonialId = c.TestimonialId,
                Username = c.User.Name,
                Messege = c.TestimonialMessege,
                Date = c.CreatedAt.HasValue
                    ? c.CreatedAt.Value.ToString("MM/dd/yyyy, HH:mm")
                    : "N/A", // في حالة كان التاريخ فارغًا (null)
                TestimonialText = c.TestimonialMessege
            }).ToList();

            return Ok(testimonials);
        }



        [HttpGet("GetAllNotAcceptedTestimonial")]
        public IActionResult NotAcceptedTestimonial()
        {
            var testimonials = _db.Testimonials
               .Include(t => t.User)  // تضمين العلاقة مع User
               .Where(t => t.IsAccept == false)
               .OrderByDescending(t => t.CreatedAt)
               .Select(t => new
               {
                   t.TestimonialId,
                   t.TestimonialMessege,
                   CreatedTestimonialAt = t.CreatedAt.HasValue
                       ? t.CreatedAt.Value.ToString("yyyy-MM-dd HH:mm")
                       : "N/A",  // التعامل مع القيمة إذا كانت null
                   UserName = t.User != null ? t.User.Name : "Unknown"  // إذا كان User موجوداً، يعرض UserName
               })
               .ToList();

            return Ok(testimonials);


        }


        [HttpPut("AcceptTestimonial/{id}")]
        public IActionResult AcceptTestimonial(int id)
        {
            if (id <= 0)
            {
                return BadRequest("You can not use 0 or negative value for id");
            }
            var testimonial = _db.Testimonials.FirstOrDefault(u => u.TestimonialId == id);
            if (testimonial == null)
            {
                return NotFound();
            }
            testimonial.IsAccept = true;
            _db.Testimonials.Update(testimonial);
            _db.SaveChanges();
            return Ok();
        }


        [HttpGet("GetAllTestimonials")]
        public IActionResult GetAllTestimonials()
        {
            var testimonials = _db.Testimonials.ToList();

            return Ok(testimonials);
        }


        [HttpGet("GetAllTestimonialsByNew")]
        public IActionResult GetThree()
        {
            var testimonials = _db.Testimonials
       .Include(t => t.User)  // تضمين العلاقة مع User
       .OrderByDescending(t => t.CreatedAt)
       .Take(3)
       .Select(t => new
       {
           t.TestimonialId,
           t.TestimonialMessege,
           t.CreatedAt,
           UserName = t.User != null ? t.User.Name : "Unknown"  // إذا كان User موجوداً، يعرض UserName
       })
       .ToList();

            return Ok(testimonials);
        }



        [HttpDelete("DeleteTestimonial/{id}")]
        public IActionResult DeleteTestimonial(int id)
        {
            if (id <= 0)
            {
                return BadRequest("You can not use 0 or negative value for id");
            }

            var testmonial = _db.Testimonials.FirstOrDefault(u => u.TestimonialId == id);
            if (testmonial == null)
            {
                return NotFound();
            }
            _db.Testimonials.Remove(testmonial);
            _db.SaveChanges();
            return Ok();
        }
    }
}
