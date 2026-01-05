using DAL.CommunityDirectory.Models.Event;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.CommunityDirectory.Interfaces
{
    public interface IEventRepository : IGenericRepository<EventClass>
    {
        Task<IEnumerable<EventClass>> GetUpcomingEventsAsync();
    }
}
