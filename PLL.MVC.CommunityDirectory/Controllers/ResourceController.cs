using AutoMapper;
using DAL.CommunityDirectory.Models.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SLL.CommunityDirectory.DTOs;
using SLL.CommunityDirectory.Interfaces;
using System.Security.Claims;

namespace PLL.MVC.CommunityDirectory.Controllers
{
    public class ResourceController : Controller
    {
        private readonly IResourceServices _resourceService;
        private readonly ICategoryServices _categoryService;
        private readonly IMapper _mapper;

        public ResourceController(IResourceServices resourceService,
            ICategoryServices categoryService,
            IMapper mapper)
        {
            _resourceService = resourceService;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            //IEnumerable<ResourceClass> resources;
            //// Check if the user is in the Admin role
            //if (User.IsInRole("Admin"))
            //{
            //    // Admin sees EVERYTHING (Pending + Approved)
            //    resources = await _resourceService.GetAllResourcesAdminAsync();
            //}
            //else
            //{
            //    // Public sees only Approved
            //    resources = await _resourceService.GetPublicResourcesAsync();
            //}
            //return View(resources);

            // DTO version
            //var resources = await _resourceService.GetPublicResourcesAsync();
            //return View(resources);

            IEnumerable<ResourceReadDTO> resources;

            // Check if the user is logged in and has the Admin role
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
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
        public async Task<IActionResult> Create(ResourceCreateDTO resource)
        {                                       // ResourceClass BEFORE DTO
            //// Get the current logged-in user's ID
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            //if (string.IsNullOrEmpty(userId))
            //{
            //    ModelState.AddModelError("", "You must be logged in to create a resource.");
            //}
            //else
            //{
            //    resource.SubmittedById = userId;
            //}

            //// Clean validation for the complex objects
            //ModelState.Remove("category");
            //ModelState.Remove("SubmittedBy");

            //if (ModelState.IsValid)
            //{

            //    await _resourceService.AddResourceAsync(resource);
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
            //return View(resource);

            //DTO version
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                //await _resourceService.AddResourceAsync(resource);
                await _resourceService.SuggestResourceAsync(resource, userId);
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

            var resourceToEdit = _mapper.Map<ResourceCreateDTO>(resource);

            var categories = await _categoryService.GetAllCategoriesAsync();

            if (categories == null)
            {
                // Handle empty categories case to prevent crash
                categories = new List<CategoryDTO>();
            }

            //ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name", resourceToEdit.CategoryId);
            //return View(resource);
            return View(resourceToEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ResourceCreateDTO resource)
        {                                               // ResourceClass BEFORE DTO
            //if (id != resource.Id) return NotFound();

            //if (ModelState.IsValid)
            //{
            //    await _resourceService.UpdateResourceAsync(resource);
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
            //return View(resource);

            if (id != resource.Id) return NotFound();
            //DTO version
            if (ModelState.IsValid)
            {
                await _resourceService.UpdateResourceAsync(id, resource); // Pass id and dto
                return RedirectToAction(nameof(Index));
            }
            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name", resource.CategoryId);
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
