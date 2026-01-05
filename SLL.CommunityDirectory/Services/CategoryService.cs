using DAL.CommunityDirectory.Interfaces;
using DAL.CommunityDirectory.Models.Resources;
using SLL.CommunityDirectory.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLL.CommunityDirectory.Services
{
    public class CategoryService : ICategoryServices
    {
        private readonly ICategoryRepository _categoryRepo;
        public CategoryService(ICategoryRepository categoryRepo) => _categoryRepo = categoryRepo;

        public async Task UpdateCategoryAsync(CategoryClass category)
        {
            await _categoryRepo.UpdateAsync(category);
        }
        public async Task<IEnumerable<CategoryClass>> GetAllCategoriesAsync() => await _categoryRepo.GetAllAsync();
        public async Task<CategoryClass> GetCategoryByIdAsync(int id) => await _categoryRepo.GetByIdAsync(id);
        public async Task CreateCategoryAsync(CategoryClass category) => await _categoryRepo.AddAsync(category);

        public async Task DeleteCategoryAsync(int id)
        {
            await _categoryRepo.DeleteAsync(id);
        }
    }
}
