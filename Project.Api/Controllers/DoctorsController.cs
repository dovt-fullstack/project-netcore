using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Api.DTO;
using Project.Api.Models;

namespace Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public  DoctorsController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> CreateDoctor([FromBody] CreateDoctorDTO model)
        {
          
            var specialty = await _context.Specialties.FindAsync(model.SpecialtyID);
            if (specialty == null)
            {
                return NotFound("specialty not found");
            }
            var newDoctor = new Doctors
            {
                DoctorName = model.DoctorName,
                SpecialtyID = model.SpecialtyID,
                //specialty
            };
            _context.Doctors.Add(newDoctor);
            await _context.SaveChangesAsync();

            var response = new PostDoctorResponse
            {
                DoctorId = newDoctor.DoctorId,
                DoctorName = newDoctor.DoctorName,
            };
            return Ok(response);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorsDTO>>> GetAllDoctors()
        {
            var doctors = await _context.Doctors
                                       .Include(u => u.Specialty)
                                       .ToListAsync();

            if (doctors == null || doctors.Count == 0)
            {
                return NotFound();
            }
            var doctorDTO = doctors.Select(doctor => new DoctorsDTO
            {
                Id = doctor.DoctorId,
                DoctorName = doctor.DoctorName,
                Specialty = doctor.Specialty.SpecialtyName

            }).ToList();
            return doctorDTO;

        }
    }
}
