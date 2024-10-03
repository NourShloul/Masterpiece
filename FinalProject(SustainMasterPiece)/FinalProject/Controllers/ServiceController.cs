using FinalProject.DTO;
using FinalProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly MyDbContext _db;


        public ServiceController(MyDbContext db)
        {
            _db = db;
        }

        [HttpGet("Service/GetAllServices")]
        public IActionResult GetAllServices()
        {
            var service = _db.Services.ToList();
            if (service != null)
            {
                return Ok(service);
            }
            return NoContent();
        }

        [HttpGet("Services/GetServiceById/{id}")]
        public IActionResult GetServiceById(int id)
        {

            if (id <= 0)
            {
                return BadRequest();

            }

            var services = _db.Services.Where(p => p.Id == id).FirstOrDefault();

            if (services != null)
            {
                return Ok(services);

            }
            return NotFound();
        }


        [HttpDelete("Services/DeleteService/{id}")]
        public IActionResult Delete(int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }
            var categories = _db.Services.FirstOrDefault(p => p.Id == id);
            if (categories != null)
            {

                _db.Services.Remove(categories);
                _db.SaveChanges();
                return NoContent();

            }
            return NotFound();

        }

        [HttpPost("Services/CreateService")]
        public IActionResult CreateService([FromForm] ServiceRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();

            }
            var service = new Service
            {
                Name = request.Name,
                Description = request.Description
            };

            _db.Services.Add(service);
            _db.SaveChanges();
            return Ok(service);

        }


        [HttpPut("Services/UpdateService/{id:int}")]
        public IActionResult UpdateService([FromForm] ServiceRequestDTO response, int id)
        {
            var service = _db.Services.FirstOrDefault(c => c.Id == id);
            if (service == null) return NotFound();
            service.Name = response.Name;
            service.Description = response.Description;

            _db.Services.Update(service);
            _db.SaveChanges();
            return Ok(service);
        }
    }
}
