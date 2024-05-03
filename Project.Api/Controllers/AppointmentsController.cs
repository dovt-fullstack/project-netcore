using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Api.DTO;
using Project.Api.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly JsonSerializerOptions _jsonOptions;

        public AppointmentsController(AppDbContext context)
        {
            _context = context;
            _jsonOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> CreateBooking(int userId, [FromBody] CreateAppointmentsDTO model)
        {
             //body
//            {
//                "userID": 5,
//  "doctorID": 6,
//  "clinicID": 2,
//  "appointmentDate": "2024-05-03T07:55:32.075Z",
//  "status": "string",
//  "serviceIDs": [
//    "5"
//  ]
//}
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var appointment = new Appointments
            {
                UserID = userId,
                DoctorID = model.DoctorID,
                ClinicID = model.ClinicID,
                AppointmentDate = model.AppointmentDate,
                Status = "Scheduled",
                Services = new List<Services>()
            };

            foreach (var serviceId in model.ServiceIDs)
            {
               
                if (int.TryParse(serviceId, out int id))
                {
                    var service = await _context.Services.FindAsync(id);
                    if (service != null)
                    {
                        appointment.Services.Add(service);
                    }
                    else
                    {
                        return BadRequest("Service not found");
                    }
                }
                else
                {
                    return BadRequest("Invalid service ID format");
                }
            }

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return Ok(JsonSerializer.Serialize(appointment, _jsonOptions));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetAppointment(int id)
        {
          
            var appointment = await _context.Appointments
                .Include(a => a.User)
                .Include(a => a.Doctor)
                .Include(a => a.Clinic)
                .Include(a => a.Services)
                .FirstOrDefaultAsync(a => a.AppointmentsId == id);

            if (appointment == null)
            {
                return NotFound();
            }
            var totalCost = appointment.Services.Sum(s => s.Cost);
            var serviceDTOs = appointment.Services.Select(s => new ServiceDetailsDTO
            {
                ServiceId = s.ServiceId,
                ServiceName = s.ServiceName,
                Cost = s.Cost
            }).ToList();
            var appointmentDTO = new AppointmentDTO
            {
                AppointmentId = appointment.AppointmentsId,
                UserName = appointment.User.UserName,
                DoctorName = appointment.Doctor.DoctorName,
                ClinicName = appointment.Clinic.ClinicName,
                AppointmentDate = appointment.AppointmentDate,
                Status = appointment.Status,
                Service = serviceDTOs,
                Cost = totalCost,
            };
            var jsonAppointment = JsonSerializer.Serialize(appointment, _jsonOptions);

            return Ok(appointmentDTO);
        }

        [HttpPost("update/{id}")]

        public async Task<IActionResult> UpdateStatusBooking(int id , [FromBody] UpdateAppointmentDTO model)
        {
            var dataBook = await _context.Appointments.FindAsync(id);
            if(dataBook == null)
            {
                return NotFound();
            }
            dataBook.Status = model.status;
            await _context.SaveChangesAsync();
            return Ok(dataBook);
        }
        [HttpGet("{userId}/bookings")]
        public async Task<ActionResult<IEnumerable<Appointments>>> GetUserBookings(int userId)
        {
            var userBookings = await _context.Appointments
                .Where(a => a.UserID == userId)
                .ToListAsync();

            if (userBookings == null || userBookings.Count == 0)
            {
                return NotFound();
            }

            return userBookings;
        }
        [HttpGet("{doctorId}/selected-users")]
        public async Task<ActionResult<IEnumerable<Appointments>>> GetDoctorAppointmentsCount(int doctorId)
        {
            var appointmentsCount = await _context.Appointments
                .Where(a => a.DoctorID == doctorId).ToListAsync();


            if (appointmentsCount == null || appointmentsCount.Count == 0)
            {
                return NotFound();
            }

            return appointmentsCount;
        }
    }
}
