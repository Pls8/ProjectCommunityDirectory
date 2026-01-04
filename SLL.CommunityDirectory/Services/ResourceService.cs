using DAL.CommunityDirectory.Interfaces;
using DAL.CommunityDirectory.Models.Resources;
using SLL.CommunityDirectory.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace SLL.CommunityDirectory.Services
{
    // SLL/Services/ResourceService.cs
    public class ResourceService : IResourceServices
    {
        private readonly IResourceRepository _resourceRepository;

        public ResourceService(IResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        public async Task<IEnumerable<ResourceClass>> GetPublicResourcesAsync()
            => await _resourceRepository.GetApprovedResourcesAsync();

        public async Task<IEnumerable<ResourceClass>> SearchAsync(string term, string? city)
            => await _resourceRepository.SearchResourcesAsync(term, city);

        public async Task<ResourceClass?> GetDetailsAsync(int id)
            => await _resourceRepository.GetByIdWithCategoryAsync(id);

        public async Task<ResourceClass> SuggestResourceAsync(ResourceClass resource)
        {
            resource.IsApproved = false;
            resource.CreatedAt = DateTime.Now;
            return await _resourceRepository.AddAsync(resource);
        }

        public async Task ApproveResourceAsync(int id)
        {
            var resource = await _resourceRepository.GetByIdAsync(id);
            if (resource != null)
            {
                resource.IsApproved = true;
                await _resourceRepository.UpdateAsync(resource);
            }
        }

        public async Task UpdateResourceAsync(ResourceClass resource) => await _resourceRepository.UpdateAsync(resource);
        public async Task DeleteResourceAsync(int id) => await _resourceRepository.DeleteAsync(id);
        

    }
}
