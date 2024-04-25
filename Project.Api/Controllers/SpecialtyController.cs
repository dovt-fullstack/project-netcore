using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Api.DTO;
using Project.Api.Models;
using System.Data;

namespace Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialtyController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SpecialtyController(AppDbContext context) {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> CreateSpecialty([FromBody] CreateSpecialtyDTO model)
        {

            var newSpecialty = new Specialties
            {
                SpecialtyName = model.SpecialtyName,
            };
            _context.Specialties.Add(newSpecialty);
            await _context.SaveChangesAsync();

            return Ok(newSpecialty);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Specialties>>> GetAllSpecialty()
        {
            return await _context.Specialties.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getDetailSpecialty(int id)
        {
            var Specialty = await _context.Specialties.FindAsync(id);

            if (Specialty == null)
            {
                return NotFound();
            }

            return Ok(Specialty);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> editSpecialty(int id, [FromBody] CreateSpecialtyDTO model)
        {
            var Specialty = await _context.Specialties.FindAsync(id);

            if (Specialty == null)
            {
                return NotFound();
            }
            Specialty.SpecialtyName = model.SpecialtyName;
            await _context.SaveChangesAsync();

            return Ok(Specialty);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> removeSpecialty(int id)
        {
            var Specialty = await _context.Specialties.FindAsync(id);

            if (Specialty == null)
            {
                return NotFound();
            }
            _context.Specialties.Remove(Specialty);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
