using BLL.CommunityDirectory.Context;
using DAL.CommunityDirectory.Interfaces;
using DAL.CommunityDirectory.Models.Resources;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.CommunityDirectory.Repositories
{
    public class ResourceRepository : GenericRepository<ResourceClass>, IResourceRepository
    {
        public ResourceRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<ResourceClass>> GetAllWithCategoryAsync()
        {
            return await _dbSet.Include(r => r.category).ToListAsync();
        }

        public async Task<ResourceClass?> GetByIdWithCategoryAsync(int id)
        {
            return await _dbSet.Include(r => r.category).FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<ResourceClass>> GetApprovedResourcesAsync()
        {
            return await _dbSet.Where(r => r.IsApproved && r.IsActive)
                               .Include(r => r.category).ToListAsync();
        }

        public async Task<IEnumerable<ResourceClass>> SearchResourcesAsync(string searchTerm, string? city)
        {
            var query = _dbSet.Include(r => r.category).Where(r => r.IsApproved && r.IsActive);

            if (!string.IsNullOrEmpty(searchTerm))
                query = query.Where(r => r.Name.Contains(searchTerm) || r.Description.Contains(searchTerm));

            if (!string.IsNullOrEmpty(city))
                query = query.Where(r => r.City == city);

            return await query.ToListAsync();
        }
    }
}
