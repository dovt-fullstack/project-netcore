using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Api.DTO;
using Project.Api.Models;

namespace Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ClinicsController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> CreateClinics([FromBody] CreateClinic model)
        {
            try
            {
                var Clinics = new Clinics
                {
                    Address = model.Address,
                    ClinicName = model.ClinicName,
                    Phone = model.Phone,
                };
                _context.Clinics.Add(Clinics);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Clinics>>> GetAllClinics()
        {
            try
            {
                return await _context.Clinics.ToListAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetailsClinic(int id)
        {
            try
            {
                var dataClinic = await _context.Clinics.FindAsync(id);
                if(dataClinic == null)
                {
                    return NotFound();
                }
                return Ok(dataClinic);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveClinic (int id)
        {
            try
            {
                var dataClinic = await _context.Clinics.FindAsync(id);
                if (dataClinic == null)
                {
                    return NotFound();
                }
                _context.Clinics.Remove(dataClinic);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditClinic(int id, [FromBody] CreateClinic model)
        {
            try
            {
                var dataClinic = await _context.Clinics.FindAsync(id);
                if (dataClinic == null)
                {
                    return NotFound();
                }
                dataClinic.Address = model.Address;
                dataClinic.Phone = model.Phone;
                dataClinic.ClinicName = model.ClinicName;
                await _context.SaveChangesAsync();
                return Ok(dataClinic);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
    }
}
