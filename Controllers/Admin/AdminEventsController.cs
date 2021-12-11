using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Events.Models;
using System.Web.Http;


namespace Events.Controllers
{
    [Route("api/v1/admin/events")]
    [ApiController]
    [Authorize]
    public class AdminEventsController : AdminAbstractController
    {
        protected readonly EventsContext? _context;

        public AdminEventsController(EventsContext context, IHttpContextAccessor httpContextAccessor, ILogger<AdminEventsController> logger)
        {
            _context = context;
            bootUser(context, httpContextAccessor, logger);
        }

        // PUT: api/Events/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(int id, EventNewDto @eventNewDto)
        {
            var @eventDB = await _context.Event.FindAsync(id);
            if (@eventDB == null)
            {
                return NotFound();
            }

            if (@eventDB.Owner != _loggedUser)
            {
                return Forbid("You can't edit not your own Event");
            }

            @eventDB.Description = @eventNewDto.Description;
            @eventDB.EndTime = @eventNewDto.EndTime;
            @eventDB.Location = @eventNewDto.Location;
            @eventDB.Name = @eventNewDto.Name;
            @eventDB.StartTime = @eventNewDto.StartTime;

            _context.Entry(@eventDB).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Events
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent(Event @event)
        {
            @event.Owner = _loggedUser;
            _context.Event.Add(@event);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvent", "Events", new { id = @event.Id }, @event);
        }

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var @event = await _context.Event.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            if (@event.Owner != _loggedUser)
            {
                return Forbid("You can't delete not your own Event");
            }

            _context.Event.Remove(@event);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Events/5
        [HttpGet("registration/{id}")]
        public async Task<ActionResult<RegistrationResponseListDto>> GetAllEventRegistration(int id)
        {
            var @event = await _context.Event.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            if (@event.Owner != _loggedUser)
            {
                return Forbid("You can't see not your own Event registrations");
            }
            var query = _context.Registration.Where(r => r.Event == @event).Select(x => x);

            var list = query.ToList();

            List<RegistrationResponseDto> registrationResponseDto = new List<RegistrationResponseDto>();

            foreach (var registration in list)
            {
                registrationResponseDto.Add(new RegistrationResponseDto
                {
                    Id = registration.Id,
                    Email = registration.Email,
                    Name = registration.Name,
                    Phone = registration.Phone
                });
            }

            return new RegistrationResponseListDto
            {
                count = query.Count(),

                Registrations = registrationResponseDto
            };
        }

        private bool EventExists(int id)
        {
            return _context.Event.Any(e => e.Id == id);
        }
    }
}
