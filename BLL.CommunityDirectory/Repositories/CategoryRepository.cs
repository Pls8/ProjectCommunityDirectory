using BLL.CommunityDirectory.Context;
using DAL.CommunityDirectory.Interfaces;
using DAL.CommunityDirectory.Models.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.CommunityDirectory.Repositories
{
    public class CategoryRepository : GenericRepository<CategoryClass>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context) { }
    }
}
