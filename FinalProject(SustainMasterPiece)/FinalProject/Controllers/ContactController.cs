using FinalProject.DTO;
using FinalProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly MyDbContext _db;
        public ContactController(MyDbContext db)
        {
            _db = db;
        }
        [HttpPost("AddMessage")]
        public IActionResult PostContactUs([FromBody] ContactRequestDTO ContactRequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contactUs = new ContactU
            {
                Name = ContactRequestDTO.Name,
                Email = ContactRequestDTO.Email,
                PhoneNumber = ContactRequestDTO.PhoneNumber,
                Message = ContactRequestDTO.Message,

            };

            _db.ContactUs.Add(contactUs);
            _db.SaveChanges();
            Shared.EmailSender.SendEmail(ContactRequestDTO.Email, "Confirmation", "Thank you for contacting us. We will get back to you soon.");

            return Ok(new { message = "Contact form submitted successfully" });
        }

        [HttpGet("GetMessages")]
        public IActionResult GetContactMessages()
        {
            var messages = _db.ContactUs
                              .OrderBy(m => m.CreatedAt)
                              .ToList();

            return Ok(messages);
        }

        [HttpDelete("DeleteMessage/{id}")]
        public IActionResult DeleteMessage(int id)
        {

            var message = _db.ContactUs.FirstOrDefault(m => m.Id == id);


            if (message == null)
            {
                return NotFound();
            }


            _db.ContactUs.Remove(message);
            _db.SaveChanges();


            return Ok(new { message = "Message deleted successfully." });
        }
    }
}
