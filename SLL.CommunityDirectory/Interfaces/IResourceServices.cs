using DAL.CommunityDirectory.Models.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLL.CommunityDirectory.Interfaces
{
    public interface IResourceServices
    {
        Task<IEnumerable<ResourceClass>> GetPublicResourcesAsync();
        Task<IEnumerable<ResourceClass>> SearchAsync(string term, string? city);
        Task<ResourceClass?> GetDetailsAsync(int id);

        Task<ResourceClass> SuggestResourceAsync(ResourceClass resource);

        Task ApproveResourceAsync(int id);
        Task UpdateResourceAsync(ResourceClass resource);
        Task DeleteResourceAsync(int id);

        Task AddResourceAsync(ResourceClass resource);
    }
}
