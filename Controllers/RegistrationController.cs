using Microsoft.AspNetCore.Mvc;
using Events.Models;

namespace Events.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly EventsContext _context;

        public RegistrationController(EventsContext context)
        {
            _context = context;
        }

        // POST: api/Registration
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Registration>> PostRegistration(RegistrationDto registrationDto)
        {
            var @event = _context.Event.Find(registrationDto.EventId);

            if (@event == null)
            {
                return NotFound();
            }

            var registration = new Registration
            {
                Email = registrationDto.Email,
                Name = registrationDto.Name,
                Phone = registrationDto.Phone,
                Event = @event
            };

            _context.Registration.Add(registration);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
