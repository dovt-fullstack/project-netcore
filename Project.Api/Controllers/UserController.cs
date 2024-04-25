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
                Avatar = user.Avatar,
                Email = user.Email,
                Address = user.Address,
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
                Avatar = user.Avatar,
                Email = user.Email,
                Address = user.Address,
                Phone = user.Phone.ToString(), 
                RoleName = user.Role?.Name 
            };
            return userDTO;
        }

        // POST: api/users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        // PUT: api/users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
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
    }
}
