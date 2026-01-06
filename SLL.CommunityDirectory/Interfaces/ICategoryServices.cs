using DAL.CommunityDirectory.Models.Resources;
using SLL.CommunityDirectory.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLL.CommunityDirectory.Interfaces
{
    public interface ICategoryServices
    {
        //Task<IEnumerable<CategoryClass>> GetAllCategoriesAsync();
        //Task UpdateCategoryAsync(CategoryClass category);
        //Task<CategoryClass> GetCategoryByIdAsync(int id);
        //Task CreateCategoryAsync(CategoryClass category);
        //Task DeleteCategoryAsync(int id);

        //DTOs
        Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();
        Task<CategoryDTO?> GetCategoryByIdAsync(int id);
        Task<CategoryDTO> CreateCategoryAsync(CategoryDTO categoryDto);
        Task UpdateCategoryAsync(int id, CategoryDTO categoryDto);
        Task DeleteCategoryAsync(int id);
    }
}
