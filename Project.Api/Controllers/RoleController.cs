using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Project.Api.Models;

namespace Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly AppDbContext _context;

        private async Task<bool> AuthenticateToken(HttpContext httpContext)
        {
            if (httpContext.Request.Headers.TryGetValue("Authorization", out StringValues token))
            {
                return true; 
            }
            return false;
        }
        private async Task<bool> RequireTokenMiddleware(HttpContext httpContext, Func<Task> next)
        {
            if (!await AuthenticateToken(httpContext))
            {
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return false;
            }
            await next();
            return true;
        }
        public RoleController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
             
            return await _context.Roles.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRole(int id)
        {
            var role = await _context.Roles.FindAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            return role;
        }
        [HttpPost]
        public async Task<ActionResult<Role>> PostRole(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRole), new { id = role.Id }, role);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(int id, Role role)
        {
            
            var roleFind = await _context.Roles.FindAsync(id);
            if (roleFind != null)
            {
                roleFind.Name = role.Name;
                
                await _context.SaveChangesAsync();
                return Ok(roleFind);
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    
       
    }
}
