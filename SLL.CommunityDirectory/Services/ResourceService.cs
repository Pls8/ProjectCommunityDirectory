using AutoMapper;
using DAL.CommunityDirectory.Interfaces;
using DAL.CommunityDirectory.Models.Resources;
using SLL.CommunityDirectory.DTOs;
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
        #region Old Code
        //private readonly IResourceRepository _resourceRepository;

        //public ResourceService(IResourceRepository resourceRepository)
        //{
        //    _resourceRepository = resourceRepository;
        //}

        //public async Task<IEnumerable<ResourceClass>> GetPublicResourcesAsync()
        //    => await _resourceRepository.GetApprovedResourcesAsync();

        //public async Task<IEnumerable<ResourceClass>> SearchAsync(string term, string? city)
        //    => await _resourceRepository.SearchResourcesAsync(term, city);

        //public async Task<ResourceClass?> GetDetailsAsync(int id)
        //    => await _resourceRepository.GetByIdWithCategoryAsync(id);

        //public async Task<ResourceClass> SuggestResourceAsync(ResourceClass resource)
        //{
        //    resource.IsApproved = false;
        //    resource.CreatedAt = DateTime.Now;
        //    return await _resourceRepository.AddAsync(resource);
        //}

        //public async Task ApproveResourceAsync(int id)
        //{
        //    var resource = await _resourceRepository.GetByIdAsync(id);
        //    if (resource != null)
        //    {
        //        resource.IsApproved = true;
        //        await _resourceRepository.UpdateAsync(resource);
        //    }
        //}

        //public async Task UpdateResourceAsync(ResourceClass resource) => await _resourceRepository.UpdateAsync(resource);
        //public async Task DeleteResourceAsync(int id) => await _resourceRepository.DeleteAsync(id);




        //public async Task AddResourceAsync(ResourceClass resource)
        //{
        //    await _resourceRepository.AddAsync(resource);
        //}

        //public async Task<IEnumerable<ResourceClass>> GetAllResourcesAdminAsync() =>
        //    await _resourceRepository.GetAllWithCategoryAsync();
        #endregion

        private readonly IResourceRepository _resourceRepository;
        private readonly IMapper _mapper;

        public ResourceService(IResourceRepository resourceRepository, IMapper mapper)
        {
            _resourceRepository = resourceRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ResourceReadDTO>> GetPublicResourcesAsync()
        {
            var resources = await _resourceRepository.GetApprovedResourcesAsync();
            return _mapper.Map<IEnumerable<ResourceReadDTO>>(resources);
        }

        public async Task<IEnumerable<ResourceReadDTO>> GetAllResourcesAdminAsync()
        {
            var resources = await _resourceRepository.GetAllWithCategoryAsync();
            return _mapper.Map<IEnumerable<ResourceReadDTO>>(resources);
        }

        public async Task<IEnumerable<ResourceReadDTO>> SearchAsync(string term, string? city)
        {
            var resources = await _resourceRepository.SearchResourcesAsync(term, city);
            return _mapper.Map<IEnumerable<ResourceReadDTO>>(resources);
        }

        public async Task<ResourceReadDTO?> GetDetailsAsync(int id)
        {
            var resource = await _resourceRepository.GetByIdWithCategoryAsync(id);
            return _mapper.Map<ResourceReadDTO>(resource);
        }

        public async Task<ResourceReadDTO> SuggestResourceAsync(ResourceCreateDTO resourceDto, string? userId)
        {
            // Map DTO to Database Class
            var resource = _mapper.Map<ResourceClass>(resourceDto);

            // Set business logic fields
            resource.IsApproved = false;
            resource.CreatedAt = DateTime.Now;

            var savedResource = await _resourceRepository.AddAsync(resource);

            // Map back to ReadDTO for the UI/API response
            return _mapper.Map<ResourceReadDTO>(savedResource);
        }

        public async Task<ResourceReadDTO> AddResourceAsync(ResourceCreateDTO resourceDto)
        {
            var resource = _mapper.Map<ResourceClass>(resourceDto);
            resource.IsApproved = true; // Admin adding is usually pre-approved
            resource.CreatedAt = DateTime.Now;

            var savedResource = await _resourceRepository.AddAsync(resource);
            return _mapper.Map<ResourceReadDTO>(savedResource);
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

        public async Task UpdateResourceAsync(int id, ResourceCreateDTO resourceDto)
        {
            var existingResource = await _resourceRepository.GetByIdAsync(id);
            if (existingResource != null)
            {
                // Map values from DTO onto the EXISTING database object
                _mapper.Map(resourceDto, existingResource);
                await _resourceRepository.UpdateAsync(existingResource);
            }
        }

        public async Task DeleteResourceAsync(int id)
        {
            await _resourceRepository.DeleteAsync(id);
        }

    }
}
