using DAL.CommunityDirectory.Models.Event;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SLL.CommunityDirectory.DTOs;
using SLL.CommunityDirectory.Interfaces;

namespace PLL.API.CommunityDirectory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : BaseController
    {
        private readonly IEventServices _eventService;

        public EventsController(IEventServices eventService)
        {
            _eventService = eventService;
        }

        // GET: api/Events
        [HttpGet]
        public async Task<IActionResult> GetEvents()
            => Ok(await _eventService.GetAllEventsAsync());

        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(int id)
        {
            var @event = await _eventService.GetEventDetailsAsync(id);
            return @event == null ? NotFound() : Ok(@event);
        }

        // POST: api/Events (Authenticated Users)
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EventDTO @event)
        {                                                   //CategoryClass BEFORE DTO
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _eventService.CreateEventAsync(@event);
            return CreatedAtAction(nameof(GetDetails), new { id = @event.Id }, @event);
        }

        // DELETE: api/Events/ (Admin Only)
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _eventService.DeleteEventAsync(id);
            return NoContent();
        }
    }
}
