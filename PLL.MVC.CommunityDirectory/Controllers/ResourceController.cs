using DAL.CommunityDirectory.Models.Resources;
using Microsoft.AspNetCore.Mvc;
using SLL.CommunityDirectory.Interfaces;
using System.Security.Claims;

namespace PLL.MVC.CommunityDirectory.Controllers
{
    public class ResourceController : Controller
    {
        private readonly IResourceServices _resourceService;
        private readonly ICategoryServices _categoryService;

        public ResourceController(IResourceServices resourceService, ICategoryServices categoryService)
        {
            _resourceService = resourceService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<ResourceClass> resources;

            // Check if the user is in the Admin role
            if (User.IsInRole("Admin"))
            {
                // Admin sees EVERYTHING (Pending + Approved)
                resources = await _resourceService.GetAllResourcesAdminAsync();
            }
            else
            {
                // Public sees only Approved
                resources = await _resourceService.GetPublicResourcesAsync();
            }

            return View(resources);
        }


        public async Task<IActionResult> Details(int id)
        {
            var resource = await _resourceService.GetDetailsAsync(id);
            if (resource == null) return NotFound();
            return View(resource);
        }



        // GET: Resource/Create
        public async Task<IActionResult> Create()
        {

            ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ResourceClass resource)
        {
            // Get the current logged-in user's ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                ModelState.AddModelError("", "You must be logged in to create a resource.");
            }
            else
            {
                resource.SubmittedById = userId;
            }

            // Clean validation for the complex objects
            ModelState.Remove("category");
            ModelState.Remove("SubmittedBy");

            if (ModelState.IsValid)
            {

                await _resourceService.AddResourceAsync(resource);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
            return View(resource);
        }

        // GET: Resource/Edit/
        public async Task<IActionResult> Edit(int id)
        {
            var resource = await _resourceService.GetDetailsAsync(id);
            if (resource == null) return NotFound();

            ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
            return View(resource);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ResourceClass resource)
        {
            if (id != resource.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _resourceService.UpdateResourceAsync(resource);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
            return View(resource);
        }

        // GET: Resource/Delete/
        public async Task<IActionResult> Delete(int id)
        {
            var resource = await _resourceService.GetDetailsAsync(id);
            if (resource == null) return NotFound();
            return View(resource);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _resourceService.DeleteResourceAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
