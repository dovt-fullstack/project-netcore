using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Api.DTO;
using Project.Api.Models;

namespace Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ServicesController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> CreateService([FromBody] CreateServices model)
        {
            try
            {
                var newService = new Services
                {
                    ServiceName = model.ServiceName,
                    Description = model.Description,
                    Cost = model.Cost
                };
                _context.Services.Add(newService);
                await _context.SaveChangesAsync();

                var respone = new CreateServices
                {
                    ServiceName = model.ServiceName,
                    Description = model.Description,
                    Cost = model.Cost
                };
                return Ok(respone);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Services>>> GetAllServices()
        {
            try
            {
                return await _context.Services.ToListAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetailsServices(int id)
        {
            try
            {
                var dataService = await _context.Services.FindAsync(id);
                if(dataService == null)
                {
                    return NotFound();
                }

                return Ok(dataService);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveService(int id)
        {
            try
            {
                var dataService = await _context.Services.FindAsync(id);
                if (dataService == null)
                {
                    return NotFound();
                }
                _context.Services.Remove(dataService);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditService(int id , [FromBody] CreateServices model)
        {
            try 
            {
                var dataService = await _context.Services.FindAsync(id);
                if(dataService == null)
                {
                    return NotFound();
                }
                dataService.ServiceName = model.ServiceName;
                dataService.Description = model.Description;
                dataService.Cost = model.Cost;
                await _context.SaveChangesAsync();
                return Ok(dataService);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    
    }
}
