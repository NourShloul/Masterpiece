using FinalProject.DTO;
using FinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MyDbContext _db;
        public UserController(MyDbContext db)
        {
            _db = db;
        }
        [HttpGet("getAllUsers")]
        public IActionResult GetAllUser()
        {
            var users = _db.Users.ToList();
            return Ok(users);
        }
        [HttpGet("getUserByID/{id:int}")]
        public IActionResult GetUser(int id)
        {
            var user = _db.Users.Find(id);
            return Ok(user);
        }

        [HttpPost("Register")]
        public IActionResult Register([FromForm] RegisterDTO newUser)
        {
            byte[] passwordHash, passwordSalt;
            PasswordHasher.CreatePasswordHash(newUser.Password, out passwordHash, out passwordSalt);
            User user = new User
            {
                Name = newUser.Name,
                Email = newUser.Email,
                Password = newUser.Password,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                PhoneNumber = newUser.PhoneNumber,
                Address = newUser.Address,
            };
            _db.Users.Add(user);
            _db.SaveChanges();
            return Ok(user);
        }
        [HttpPost("Login")]
        public IActionResult Login([FromForm] LoginDTO user)
        {
            // Check if email is provided
            if (string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Password))
            {
                return BadRequest("Please enter your email.");
            }

            // Retrieve user data based on email
            var data = _db.Users.FirstOrDefault(x => x.Email == user.Email && x.Password == user.Password );
            if (data == null)
            {
                return BadRequest("You should register first.");
            }

            // If the email exists, return a success message with the user ID
            return Ok(new { message = "Login successfully", userId = data.Id }); // Ensure 'Id' corresponds to the user's unique identifier
        }



        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromForm] EmailRequest request)
        {
            // Generate OTP
            var otp = OTPGenerator.GenerateOtp();
            var user = _db.Users.FirstOrDefault(x => x.Email == request.ToEmail);
            if (user == null) return NotFound();
            user.Password = otp;
            await _db.SaveChangesAsync();

            // Create email body including the OTP
            var emailBody = $"Hello Dear, Your SmartTech OTP code for resetting your password is: {otp} Thank you.";
            const string subject = "send OTP";
            // Send email with OTP
            //await _emailService.SendEmailAsync(request.ToEmail, Subject, emailBody);
            Shared.EmailSender.SendEmail(request.ToEmail, subject, emailBody);

            return Ok(new { message = "Email sent successfully.", otp, user.Id }); // Optionally return the OTP for testing
        }
        [HttpPost("GetOTP/{id}")]
        public IActionResult GetOtp([FromForm] OTPDTO request, int id)
        {
            var user = _db.Users.Find(id);
            if (user?.Password == request.OTP)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
