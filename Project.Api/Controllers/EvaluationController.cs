using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Api.DTO;
using Project.Api.Models;

namespace Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluationController : ControllerBase
    {
        private readonly AppDbContext _context;
        public EvaluationController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> CreateEvaluation([FromBody] CreateEvaluationDTO model)
        {
            var data = await _context.Appointments.FindAsync(model.AppointmentsId);
            if (data == null)
            {
                return BadRequest("Appointments not found");
            }
            var Evaluation = new Evaluation
            {
                AppointmentsId = model.AppointmentsId,
                Content = model.Content,
                Star = model.Star,
                UserID = model.UserID,
            };
            _context.Evaluation.Add(Evaluation);
            await _context.SaveChangesAsync();
            return Ok("Created");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetIdEvaluation(int id)
        {
            var data = await _context.Evaluation.FindAsync(id);
            if (data == null)
            {
                return BadRequest("Evaluation not found");
            }
            var dataUser = await _context.Users.FindAsync(data.UserID);

            var respone = new EvaluationDTO
            {
                idEvaluation = id,
                appointment = data.AppointmentsId,
                Content = data.Content,
                Star = data.Star,
                User = dataUser.UserName
            };
            return Ok(respone);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Evaluation>>> GetAllEvaluation()
        {
            return await _context.Evaluation
                                       .ToListAsync();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveEvaluation(int id)
        {
            var Evaluation = await _context.Evaluation.FindAsync(id);
            if (Evaluation == null)
            {
                return NotFound();
            }
            _context.Evaluation.Remove(Evaluation);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditEvaluation(int id, [FromBody] EditEval model)
        {
            var Evaluation = await _context.Evaluation.FindAsync(id);
            if (Evaluation == null)
            {
                return NotFound();
            }
            Evaluation.Star = model.Star;
            Evaluation.Content = model.Content;
             await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("by-AppointmentsId/{id}")]
        public async Task<IActionResult> GetEvalAppointmentsId(int id)
        {
            var evaluations = await _context.Evaluation
                                            .Include(e => e.Appointments) 
                                            .ThenInclude(a => a.User)     
                                            .Where(e => e.AppointmentsId == id)
                                            .ToListAsync();

            if (evaluations == null || !evaluations.Any())
            {
                return NotFound();
            }
            var evaluationDTOs = new List<EvaluationDTO>();

            foreach (var evaluation in evaluations)
            {
                var evaluationDTO = new EvaluationDTO
                {
                    User = evaluation.Appointments.User.UserName,
                    appointment = evaluation.AppointmentsId,
                    Content = evaluation.Content,
                    Star = evaluation.Star,
                    idEvaluation = evaluation.IdEvaluation
                };
                evaluationDTOs.Add(evaluationDTO);
            }
            return Ok(evaluationDTOs);
        }
    }
}
