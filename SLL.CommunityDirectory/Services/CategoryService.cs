using AutoMapper;
using DAL.CommunityDirectory.Interfaces;
using DAL.CommunityDirectory.Models.Resources;
using SLL.CommunityDirectory.DTOs;
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
        //private readonly ICategoryRepository _categoryRepo;
        //public CategoryService(ICategoryRepository categoryRepo) => _categoryRepo = categoryRepo;

        //public async Task UpdateCategoryAsync(CategoryClass category)
        //{
        //    await _categoryRepo.UpdateAsync(category);
        //}
        //public async Task<IEnumerable<CategoryClass>> GetAllCategoriesAsync() => await _categoryRepo.GetAllAsync();
        //public async Task<CategoryClass> GetCategoryByIdAsync(int id) => await _categoryRepo.GetByIdAsync(id);
        //public async Task CreateCategoryAsync(CategoryClass category) => await _categoryRepo.AddAsync(category);

        //public async Task DeleteCategoryAsync(int id)
        //{
        //    await _categoryRepo.DeleteAsync(id);
        //}



        private readonly ICategoryRepository _categoryRepo;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public async Task<CategoryDTO?> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepo.GetByIdAsync(id);
            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<CategoryDTO> CreateCategoryAsync(CategoryDTO categoryDto)
        {
            var category = _mapper.Map<CategoryClass>(categoryDto);
            var result = await _categoryRepo.AddAsync(category);
            return _mapper.Map<CategoryDTO>(result);
        }

        public async Task UpdateCategoryAsync(int id, CategoryDTO categoryDto)
        {
            var existingCategory = await _categoryRepo.GetByIdAsync(id);
            if (existingCategory != null)
            {
                _mapper.Map(categoryDto, existingCategory);
                await _categoryRepo.UpdateAsync(existingCategory);
            }
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await _categoryRepo.DeleteAsync(id);
        }
    }
}
