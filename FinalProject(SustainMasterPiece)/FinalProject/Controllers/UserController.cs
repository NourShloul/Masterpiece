﻿using FinalProject.DTO;
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
                return Unauthorized("You should register first.");
            }

            // If the email exists, return a success message with the user ID
            return Ok(new { message = "Login successfully", userId = data.Id }); // Ensure 'Id' corresponds to the user's unique identifier
        }

    }
}
