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
                Email = model.Email,
                Password = model.Password,
                SpecialtyName = ""

            };
            _context.Doctors.Add(newDoctor);
            await _context.SaveChangesAsync();
            var response = new PostDoctorResponse
            {
                DoctorId = newDoctor.DoctorId,
                DoctorName = newDoctor.DoctorName,
                Email = newDoctor.Email,

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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetailDoctor(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);

            if (doctor == null)
            {
                return NotFound("Doctor not found");
            }

            var specialtyId = doctor.SpecialtyID; 

            var specialty = await _context.Specialties.FindAsync(specialtyId); 

            if (specialty == null)
            {
                return BadRequest("Specialty not found for the doctor");
            }
            var specialtyName = specialty.SpecialtyName; 
            var dataNew = new
            {
                Doctor = new
                {
                    doctor.DoctorId,
                    doctor.DoctorName,
                    doctor.SpecialtyID,
                    SpecialtyNameDoctor = specialtyName
                }
            };
            return Ok(dataNew);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveDoctor(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if(doctor == null)
            {
                return NotFound();
            }
            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDoctor(int id, [FromBody] CreateDoctorDTO model)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            doctor.DoctorName = model.DoctorName;
            doctor.SpecialtyID = model.SpecialtyID;
            await _context.SaveChangesAsync();

            return Ok(doctor);
        }
    }
}
