using FinalProject.DTO;
using FinalProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly MyDbContext _db;
        private readonly IConfiguration _config;
        public AccountController(MyDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        // Login API
        [HttpPost("Login")]
        public IActionResult Login([FromForm] LoginDTO userLogin)
        {
            // التحقق من الإداري أو المستخدم
            var user = _db.Users.FirstOrDefault(x => x.Email == userLogin.Email);
            var admin = _db.Admins.FirstOrDefault(x => x.Email == userLogin.Email);

            if (user == null && admin == null)
            {
                return BadRequest("You need to register first");
            }

            if (userLogin.Email == null || userLogin.Password == null)
            {
                return BadRequest("Please enter both email and password");
            }

            //// التحقق من صحة كلمة المرور
            bool isPasswordValid;
            byte[] storedPasswordHash;
            byte[] storedPasswordSalt;

            if (user != null)
            {
                storedPasswordHash = user.PasswordHash;
                storedPasswordSalt = user.PasswordSalt;
                isPasswordValid = PasswordHasher.VerifyPassword(userLogin.Password, storedPasswordHash, storedPasswordSalt);
            }
            else // في حال كان المستخدم إدارياً
            {
                storedPasswordHash = admin.PasswordHash;
                storedPasswordSalt = admin.PasswordSalt;
                isPasswordValid = PasswordHasher.VerifyPassword(userLogin.Password, storedPasswordHash, storedPasswordSalt);
            }

            if (!isPasswordValid)
            {
                return BadRequest("Incorrect email or password");
            }

            // توليد JWT token
            var token = GenerateJwtToken(user ?? (object)admin);

            return Ok(new { Token = token, Message = "Login successful" });
        }

        // دالة توليد الـ JWT Token
        private string GenerateJwtToken(object userOrAdmin)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);

            string userRole;
            string userId;

            if (userOrAdmin is User user)
            {
                userRole = "User";
                userId = user.Id.ToString();
            }
            else if (userOrAdmin is Admin admin)
            {
                userRole = "Admin";
                userId = admin.Id.ToString();
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //Subject = new ClaimsIdentity(new Claim[]
                //{
                //new Claim(ClaimTypes.NameIdentifier, userId),
                //new Claim(ClaimTypes.Email, userOrAdmin is User u ? u.Email : (userOrAdmin as Admin).Email),
                //new Claim(ClaimTypes.Role, userRole)
                //}),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}