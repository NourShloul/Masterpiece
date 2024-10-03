using FinalProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly MyDbContext _db;
        public PaymentController(MyDbContext db) {  _db = db; }

        [HttpGet("GetAllPayment")]
        public IActionResult GetAllPayments()
        {
            var payments = _db.Payments.ToList();
            return Ok(payments);
        }
        [HttpGet("GetPaymentByID{id}")]
        public IActionResult GetPaymentbyID(int id)
        {
            var payment = _db.Payments.Find(id);

            if (payment == null)
            {
                return NotFound();
            }

            return Ok(payment);
        }
        [HttpPost("CreatePayment")]
        public IActionResult CreatePayment([FromBody] Payment payment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Payments.Add(payment);
            _db.SaveChanges();

            return CreatedAtAction(nameof(GetPaymentbyID), new { id = payment.PaymentId }, payment);
        }
        //[HttpPut("EditPayment{id}")]
        //public IActionResult UpdatePayment(int id, [FromForm] PaymentDTO payment)
        //{
        //    if (id != payment.PaymentId)
        //    {
        //        return BadRequest();
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _db.Entry(payment).State = EntityState.Modified;

        //    try
        //    {
        //        _db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!PaymentExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}
        [HttpDelete("{id}")]
        public IActionResult DeletePayment(int id)
        {
            var payment = _db.Payments.Find(id);
            if (payment == null)
            {
                return NotFound();
            }

            _db.Payments.Remove(payment);
            _db.SaveChanges();

            return NoContent();
        }

        private bool PaymentExists(int id)
        {
            return _db.Payments.Any(p => p.PaymentId == id);
        }
        


    }
}
