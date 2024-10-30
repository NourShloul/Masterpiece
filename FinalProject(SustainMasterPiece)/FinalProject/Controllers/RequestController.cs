using FinalProject.DTO;
using FinalProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly MyDbContext _db;

        public RequestController(MyDbContext db)
        {
            _db = db;
        }

        // POST: api/ServiceRequests
        [HttpPost("createServiceRequest")]
        public IActionResult CreateServiceRequest([FromForm] ServiceRequestorderDTO request)
        {
            if (request == null)
            {
                return BadRequest("Invalid data.");
            }
            var order = new ServiceRequest
            {
                ContactName=request.ContactName,
                ContactEmail=request.ContactEmail,
                ContactPhone=request.ContactPhone,
                ServiceType=request.ServiceType,
                SubServices=request.SubServices,
                ProjectDescription=request.ProjectDescription,
                ProjectDuration=request.ProjectDuration,
                ExpectedBudget=request.ExpectedBudget,
                MeetingPreference=request.MeetingPreference,

            };
            _db.ServiceRequests.Add(order);
            _db.SaveChanges();

            return Ok();
        }

        [HttpPost]

        // GET: api/ServiceRequests/{id}
        [HttpGet("getServiceRequestBy/{id}")]
        public IActionResult GetServiceRequest(int id)
        {
            var request = _db.ServiceRequests.Find(id);

            if (request == null)
            {
                return NotFound();
            }

            return Ok(request);
        }
    }
}
