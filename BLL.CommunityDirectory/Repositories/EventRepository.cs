using BLL.CommunityDirectory.Context;
using DAL.CommunityDirectory.Interfaces;
using DAL.CommunityDirectory.Models.Event;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.CommunityDirectory.Repositories
{
    public class EventRepository : GenericRepository<EventClass>, IEventRepository
    {
        public EventRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<EventClass>> GetUpcomingEventsAsync()
        {
            return await _dbSet.Where(e => e.EventDate >= DateTime.Now)
                               .OrderBy(e => e.EventDate)
                               .ToListAsync();
        }
    }
}
