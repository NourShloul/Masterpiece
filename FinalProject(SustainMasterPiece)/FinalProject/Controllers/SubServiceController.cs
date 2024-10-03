using FinalProject.DTO;
using FinalProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubServiceController : ControllerBase
    {
        private readonly MyDbContext _db;


        public SubServiceController(MyDbContext db)
        {
            _db = db;
        }

        //[HttpGet("subService/GetAllsubServices")]
        //public IActionResult GetAllsubServices()
        //{
        //    var subservice = _db.SubServices.ToList();
        //    if (subservice != null)
        //    {
        //        return Ok(subservice);
        //    }
        //    return NoContent();
        //}

        [HttpGet("subService/GetAllsubServices")]
        public IActionResult GetAllSubServices()
        {
            var subServices = _db.SubServices
                .Join(_db.Services, subService => subService.ServiceId, service => service.Id,
                    (subService, service) => new
                    {
                        subService.Id,
                        subService.Name,
                        ServiceName = service.Name, // Service Name
                        ServiceId = service.Id, 
                        subService.Description,
                        subService.CreatedAt
                    }).ToList();

            return Ok(subServices);
        }

        [HttpGet("subServices/GetsubServiceById/{id}")]
        public IActionResult GetsubServiceById(int id)
        {

            if (id <= 0)
            {
                return BadRequest();

            }

            var services = _db.SubServices.Where(p => p.Id == id).FirstOrDefault();

            if (services != null)
            {
                return Ok(services);

            }
            return NotFound();
        }
        [HttpGet("subServices/GetSubServicesByServiceId/{serviceId}")]
        public IActionResult GetSubServicesByServiceId(int serviceId)
        {
            if (serviceId <= 0)
            {
                return BadRequest("Invalid Service ID");
            }

            // Fetch all subservices with the given serviceId
            var subServices = _db.SubServices.Where(p => p.ServiceId == serviceId).ToList();

            if (subServices != null && subServices.Count > 0)
            {
                return Ok(subServices); // Return the list of subservices for the service
            }

            return NotFound("No sub-services found for the given Service ID.");
        }


        [HttpDelete("subServices/DeletesubService/{id}")]
        public IActionResult Delete(int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }
            var categories = _db.SubServices.FirstOrDefault(p => p.Id == id);
            if (categories != null)
            {

                _db.SubServices.Remove(categories);
                _db.SaveChanges();
                return NoContent();

            }
            return NotFound();

        }

        [HttpPost("subServices/CreatesubService")]
        public IActionResult CreateService([FromForm] SubServiceRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            // Assuming SubService is the correct entity type for the SubServices table
            var subService = new SubService
            {
                ServiceId = request.ServiceId,
                Name = request.Name,
                Description = request.Description
            };

            _db.SubServices.Add(subService);
            _db.SaveChanges();

            return Ok(subService);
        }


        [HttpPut("subServices/UpdatesubService/{id:int}")]
        public IActionResult UpdateService([FromBody] SubServiceRequestDTO response, int id)
        {
            // Find the subservice based on the provided id
            var subservice = _db.SubServices.FirstOrDefault(c => c.Id == id);

            if (subservice == null)
                return NotFound();  // Return 404 if subservice with the given id doesn't exist

            // Update subservice fields only if necessary
            if (response.ServiceId != subservice.ServiceId)
                subservice.ServiceId = response.ServiceId;  // Update ServiceId if changed

            if (!string.IsNullOrEmpty(response.Name) && response.Name != subservice.Name)
                subservice.Name = response.Name;  // Update Name if provided and different

            if (!string.IsNullOrEmpty(response.Description) && response.Description != subservice.Description)
                subservice.Description = response.Description;  // Update Description if provided and different

            // Save changes to the database
            _db.SubServices.Update(subservice);  // Ensure correct table
            _db.SaveChanges();

            return Ok(subservice);  // Return updated subservice
        }

    }
}
