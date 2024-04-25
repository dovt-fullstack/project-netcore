using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Api.DTO;
using Project.Api.Models;

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
    }
}
