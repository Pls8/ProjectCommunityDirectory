using DAL.CommunityDirectory.Models.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenTelemetry.Resources;
using SLL.CommunityDirectory.Interfaces;


namespace PLL.API.CommunityDirectory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesController : BaseController
    {
        private readonly IResourceServices _resourceService;

        public ResourcesController(IResourceServices resourceService)
        {
            _resourceService = resourceService;
        }

        // Public Endpoints
        [HttpGet]
        public async Task<IActionResult> GetResources()
            => Ok(await _resourceService.GetPublicResourcesAsync());

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string term, [FromQuery] string? city)
            => Ok(await _resourceService.SearchAsync(term, city));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(int id)
        {
            var resource = await _resourceService.GetDetailsAsync(id);
            return resource == null ? NotFound() : Ok(resource);
        }

        // Authenticated Endpoints
        [HttpPost]
        public async Task<IActionResult> SuggestResource([FromBody] ResourceClass model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _resourceService.SuggestResourceAsync(model);
            return CreatedAtAction(nameof(GetDetails), new { id = result.Id }, result);
        }

        // Admin Endpoints
        [Authorize(Roles = "Admin")]
        [HttpPut("approve/{id}")]
        public async Task<IActionResult> Approve(int id)
        {
            await _resourceService.ApproveResourceAsync(id);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _resourceService.DeleteResourceAsync(id);
            return NoContent();
        }
    }
}
