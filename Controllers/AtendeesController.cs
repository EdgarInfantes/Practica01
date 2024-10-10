using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practica01.Data;

namespace Practica01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtendeesController : ControllerBase
    {
        private readonly EventManagementDbContext _context;

        public AtendeesController(EventManagementDbContext context)
        {
            _context = context;
        }

        // Para el Get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attendees>>> GetAttendees()
        {
            return await _context.Attendees.ToListAsync();
        }

        // Para el Post
        [HttpPost]
        public async Task<ActionResult<Attendees>> PostAttendees(Attendees attend)
        {
            _context.Attendees.Add(attend);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAttendees", new { id = attend.Id }, attend);
        }

        // Para el Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttendees(int id)
        {
            var attennd = await _context.Attendees.FindAsync(id);
            if (attennd == null)
            {
                return NotFound();
            }

            _context.Attendees.Remove(attennd);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
