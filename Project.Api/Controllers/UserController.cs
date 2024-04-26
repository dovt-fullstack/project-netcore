using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Api.DTO;
using Project.Api.Models;

namespace Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }
        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            var users = await _context.Users
                                       .Include(u => u.Role)
                                       .ToListAsync();

            if (users == null || users.Count == 0)
            {
                return NotFound();
            }

            // Map each User object to UserDTO
            var usersDTO = users.Select(user => new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Phone = user.Phone.ToString(),
                RoleName = user.Role?.Name
            }).ToList();

            return usersDTO;
        }

        // GET: api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            var user = await _context.Users
                                    .Include(u => u.Role)
                                    .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }
            var userDTO = new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Phone = user.Phone.ToString(), 
                RoleName = user.Role?.Name 
            };
            return userDTO;
        }
        // POST: api/users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(CreateUser user)
        {
            var newUser = new User
            {
                UserName = user.UserName,
                Password = user.Password,
                Email = user.Email,
                Phone = user.Phone,
                RoleId = 3
            };
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }
        // PUT: api/users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, [FromBody]  UserUpdateDTO user)
        {
            var userData = await _context.Users.FindAsync(id);
            if(user == null) { return NotFound(); }
            userData.UserName = user.UserName;
            userData.Email = user.Email;
            userData.Phone = user.Phone;
            await _context.SaveChangesAsync();

            return Ok("updated success");
        }
        // DELETE: api/users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("update/role-user/{id}")]
        public async Task<IActionResult> UpdateRoleUser(int id, [FromBody] UpdateRoleUserDTO model)
        {
            try
            {
                var role = await _context.Roles.FindAsync(id);
                var user = await _context.Users.FindAsync(model.UserId);
                if (role == null)
                {
                    return BadRequest("Role not found");
                }
                if (user == null)
                {
                    return BadRequest("User not found");
                }
                user.RoleId = id;
                await _context.SaveChangesAsync();

                return Ok("Role updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request");

            }
        }
        [HttpPost("update/password/{id}")]
        public async Task<IActionResult> UpdatePasswordUser(int id, [FromBody] UpdatePassWorduserDTO model)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    return BadRequest("User not found");
                }
                user.Password = model.PassWord;
                await _context.SaveChangesAsync();
                return Ok("Password updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request");
            }
        }
    }
}
