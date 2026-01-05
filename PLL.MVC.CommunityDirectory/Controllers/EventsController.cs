using DAL.CommunityDirectory.Models.Event;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SLL.CommunityDirectory.Interfaces;
using SLL.CommunityDirectory.Services;

namespace PLL.MVC.CommunityDirectory.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EventsController : Controller
    {
        private readonly IEventServices _eventService;
        private readonly ICategoryServices _categoryService;

        public EventsController(IEventServices eventService, ICategoryServices categoryService)
        {
            _eventService = eventService;
            _categoryService = categoryService;
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


        // GET: Events/Create
        public async Task<IActionResult> Create()
        {
            // Fetch categories and put them in a ViewBag for the dropdown
            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");
            return View();
        }

        // POST: Events/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventClass @event)
        {
            if (ModelState.IsValid)
            {
                await _eventService.CreateEventAsync(@event);
                return RedirectToAction(nameof(Index));
            }

            // If something fails, reload the dropdown so the page doesn't crash
            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name", @event.CategoryId);
            return View(@event);
        }


        // GET: Events/Edit/
        public async Task<IActionResult> Edit(int id)
        {
            var eventToEdit = await _eventService.GetEventDetailsAsync(id);
            if (eventToEdit == null)
            {
                return NotFound();
            }

            var categories = await _categoryService.GetAllCategoriesAsync();
            // Here we use eventToEdit.CategoryId to pre-select the current category
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name", eventToEdit.CategoryId);

            return View(eventToEdit);
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
