using DAL.CommunityDirectory.Models.Resources;
using SLL.CommunityDirectory.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLL.CommunityDirectory.Interfaces
{
    public interface IResourceServices
    {
        //Task<IEnumerable<ResourceClass>> GetPublicResourcesAsync();
        //Task<IEnumerable<ResourceClass>> SearchAsync(string term, string? city);
        //Task<ResourceClass?> GetDetailsAsync(int id);
        //Task<ResourceClass> SuggestResourceAsync(ResourceClass resource);
        //Task ApproveResourceAsync(int id);
        //Task UpdateResourceAsync(ResourceClass resource);
        //Task DeleteResourceAsync(int id);
        //Task AddResourceAsync(ResourceClass resource);
        //Task<IEnumerable<ResourceClass>> GetAllResourcesAdminAsync();


        //DTOs
        // Retrieval Methods (Returning ReadDTOs)
        Task<IEnumerable<ResourceReadDTO>> GetPublicResourcesAsync();
        Task<IEnumerable<ResourceReadDTO>> GetAllResourcesAdminAsync();
        Task<IEnumerable<ResourceReadDTO>> SearchAsync(string term, string? city);
        Task<ResourceReadDTO?> GetDetailsAsync(int id);

        // Action Methods (Accepting CreateDTO, returning ReadDTO)
        Task<ResourceReadDTO> SuggestResourceAsync(ResourceCreateDTO resourceDto, string? userId);
        Task<ResourceReadDTO> AddResourceAsync(ResourceCreateDTO resourceDto);

        // Management Methods
        Task ApproveResourceAsync(int id);
        Task DeleteResourceAsync(int id);

        // Update typically uses the domain class or a dedicated UpdateDTO
        Task UpdateResourceAsync(int id, ResourceCreateDTO resourceDto);


    }
}
