using DAL.CommunityDirectory.Interfaces;
using DAL.CommunityDirectory.Models.Event;
using Microsoft.Extensions.Logging;
using SLL.CommunityDirectory.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLL.CommunityDirectory.Services
{
    public class EventService : IEventServices
    {
        private readonly IEventRepository _eventRepo;
        public EventService(IEventRepository eventRepo) => _eventRepo = eventRepo;

        public async Task<IEnumerable<EventClass>> GetAllEventsAsync() => await _eventRepo.GetAllAsync();
        public async Task<IEnumerable<EventClass>> GetUpcomingEventsAsync() => await _eventRepo.GetUpcomingEventsAsync();
        public async Task<EventClass> GetEventDetailsAsync(int id) => await _eventRepo.GetByIdAsync(id);
        public async Task UpdateEventAsync(EventClass eventClass)
        {
            await _eventRepo.UpdateAsync(eventClass);
        }
        public async Task CreateEventAsync(EventClass @event) => await _eventRepo.AddAsync(@event);
        public async Task DeleteEventAsync(int id) => await _eventRepo.DeleteAsync(id);
    }
}
