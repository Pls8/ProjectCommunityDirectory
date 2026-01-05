using DAL.CommunityDirectory.Models.Resources;
using Microsoft.AspNetCore.Mvc;
using SLL.CommunityDirectory.Interfaces;

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
            var resources = await _resourceService.GetPublicResourcesAsync();
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
            if (ModelState.IsValid)
            {
                await _resourceService.AddResourceAsync(resource);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
            return View(resource);
        }

        // GET: Resource/Edit/5
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

        // GET: Resource/Delete/5
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
