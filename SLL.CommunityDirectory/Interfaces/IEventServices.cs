using DAL.CommunityDirectory.Models.Event;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLL.CommunityDirectory.Interfaces
{
    public interface IEventServices
    {
        Task<IEnumerable<EventClass>> GetAllEventsAsync();
        Task<IEnumerable<EventClass>> GetUpcomingEventsAsync();
        Task UpdateEventAsync(EventClass eventClass);
        Task<EventClass> GetEventDetailsAsync(int id);
        Task CreateEventAsync(EventClass @event);
        Task DeleteEventAsync(int id);
    }
}
