using DAL.CommunityDirectory.Models.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.CommunityDirectory.Interfaces
{
    public interface IResourceRepository : IGenericRepository<ResourceClass>
    {
        Task<IEnumerable<ResourceClass>> GetAllWithCategoryAsync();
        Task<ResourceClass?> GetByIdWithCategoryAsync(int id);
        Task<IEnumerable<ResourceClass>> GetApprovedResourcesAsync();
        Task<IEnumerable<ResourceClass>> SearchResourcesAsync(string searchTerm, string? city);
    }
}

