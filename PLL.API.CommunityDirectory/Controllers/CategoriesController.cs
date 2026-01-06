using DAL.CommunityDirectory.Models.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SLL.CommunityDirectory.DTOs;
using SLL.CommunityDirectory.Interfaces;

namespace PLL.API.CommunityDirectory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController
    {
        private readonly ICategoryServices _categoryService;

        public CategoriesController(ICategoryServices categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _categoryService.GetAllCategoriesAsync());

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            return category == null ? NotFound() : Ok(category);
        }

        // POST: api/Categories (Admin Only)
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryDTO category)
        {                                                   //CategoryClass BEFORE DTO
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _categoryService.CreateCategoryAsync(category);
            return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
        }

        // PUT: api/Categories/ (Admin Only)
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryDTO category)
        {                                                           //CategoryClass BEFORE DTO
            if (id != category.Id) return BadRequest();

            //CategoryClass BEFORE DTO
            //await _categoryService.UpdateCategoryAsync(category);

            await _categoryService.UpdateCategoryAsync(id, category);
            return NoContent();
        }

        // DELETE: api/Categories/5 (Admin Only)
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}
