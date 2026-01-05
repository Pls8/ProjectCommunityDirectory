using DAL.CommunityDirectory.Models.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLL.CommunityDirectory.Interfaces
{
    public interface ICategoryServices
    {
        Task<IEnumerable<CategoryClass>> GetAllCategoriesAsync();
        Task UpdateCategoryAsync(CategoryClass category);
        Task<CategoryClass> GetCategoryByIdAsync(int id);
        Task CreateCategoryAsync(CategoryClass category);
        Task DeleteCategoryAsync(int id);
    }
}
