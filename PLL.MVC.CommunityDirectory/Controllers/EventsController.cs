using DAL.CommunityDirectory.Models.Event;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SLL.CommunityDirectory.Interfaces;

namespace PLL.MVC.CommunityDirectory.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EventsController : Controller
    {
        private readonly IEventServices _eventService; 

        public EventsController(IEventServices eventService)
        {
            _eventService = eventService;
        }

        public async Task<IActionResult> Index()
        {
            var events = await _eventService.GetAllEventsAsync();
            return View(events);
        }


        // GET: Event/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var eventClass = await _eventService.GetEventDetailsAsync(id);
            if (eventClass == null) return NotFound();
            return View(eventClass);
        }


        // GET: Event/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventClass eventClass)
        {
            if (ModelState.IsValid)
            {
                await _eventService.CreateEventAsync(eventClass);
                return RedirectToAction(nameof(Index));
            }
            return View(eventClass);
        }


        // GET: Events/Edit/
        public async Task<IActionResult> Edit(int id)
        {
            var eventClass = await _eventService.GetEventDetailsAsync(id);
            if (eventClass == null) return NotFound();
            return View(eventClass);
        }

        // POST: Events/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EventClass eventClass)
        {
            if (id != eventClass.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _eventService.UpdateEventAsync(eventClass);
                return RedirectToAction(nameof(Index));
            }
            return View(eventClass);
        }



        // GET: Event/Delete
        public async Task<IActionResult> Delete(int id)
        {
            var eventClass = await _eventService.GetEventDetailsAsync(id);
            if (eventClass == null) return NotFound();
            return View(eventClass);
        }


        // POST: Event/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _eventService.DeleteEventAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
