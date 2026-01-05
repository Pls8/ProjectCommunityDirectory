using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SLL.CommunityDirectory.Interfaces;

namespace PLL.MVC.CommunityDirectory.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        private readonly IResourceServices _resourceService;

        public AdminController(IResourceServices resourceService)
        {
            _resourceService = resourceService;
        }

       
        public async Task<IActionResult> Dashboard()
        {
            var resources = await _resourceService.GetPublicResourcesAsync();
            return View(resources);
        }


        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            await _resourceService.ApproveResourceAsync(id);
            return RedirectToAction(nameof(Dashboard));
        }

    }
}
