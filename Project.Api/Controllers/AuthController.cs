using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Project.Api.DTO;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly AppDbContext _context;

        public AuthController(IConfiguration config, AppDbContext context)
        {
            _config = config;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            if(model.IsDoctor == false)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                var roleName = await _context.Roles.FindAsync(user.RoleId);
                var respone = new LoginResponse
                {
                    Email = user.Email,
                    phone = user.Phone,
                    roleName = roleName.Name,
                    userName = user.UserName,

                };

                if (user != null && VerifyPassword(model.Password, user.Password))
                {
                    var token = GenerateAccessToken(user.Email);
                    return Ok(new { AccessToken = token, User = respone});
                }
            }
            else
            {
                var doctor = await _context.Doctors.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (doctor != null && VerifyPassword(model.Password, doctor.Password))
                {
                    var token = GenerateAccessToken(doctor.Email);
                    return Ok(new { AccessToken = token, User = doctor, doctor = true });
                }
            }

            return Unauthorized();
        }
        private bool VerifyPassword(string password, string hashedPassword)
        {
            return password == hashedPassword;
        }
        private string GenerateAccessToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
