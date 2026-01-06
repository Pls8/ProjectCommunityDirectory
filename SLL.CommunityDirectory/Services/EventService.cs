using AutoMapper;
using DAL.CommunityDirectory.Interfaces;
using DAL.CommunityDirectory.Models.Event;
using Microsoft.Extensions.Logging;
using SLL.CommunityDirectory.DTOs;
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
        //private readonly IEventRepository _eventRepo;
        //public EventService(IEventRepository eventRepo) => _eventRepo = eventRepo;

        //public async Task<IEnumerable<EventClass>> GetAllEventsAsync() =>
        //    await _eventRepo.GetAllWithCategoryAsync();
        //public async Task<IEnumerable<EventClass>> GetUpcomingEventsAsync() => await _eventRepo.GetUpcomingEventsAsync();
        //public async Task<EventClass> GetEventDetailsAsync(int id) => await _eventRepo.GetByIdAsync(id);
        //public async Task UpdateEventAsync(EventClass eventClass)
        //{
        //    await _eventRepo.UpdateAsync(eventClass);
        //}
        //public async Task CreateEventAsync(EventClass @event) => await _eventRepo.AddAsync(@event);
        //public async Task DeleteEventAsync(int id) => await _eventRepo.DeleteAsync(id);



        private readonly IEventRepository _eventRepo;
        private readonly IMapper _mapper;

        public EventService(IEventRepository eventRepo, IMapper mapper)
        {
            _eventRepo = eventRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EventDTO>> GetAllEventsAsync()
        {
            var events = await _eventRepo.GetAllWithCategoryAsync();
            return _mapper.Map<IEnumerable<EventDTO>>(events);
        }

        public async Task<IEnumerable<EventDTO>> GetUpcomingEventsAsync()
        {
            var upcomingEvents = await _eventRepo.GetUpcomingEventsAsync();
            return _mapper.Map<IEnumerable<EventDTO>>(upcomingEvents);
        }

        public async Task<EventDTO?> GetEventDetailsAsync(int id)
        {
            var eventEntity = await _eventRepo.GetByIdAsync(id);
            return _mapper.Map<EventDTO>(eventEntity);
        }

        public async Task<EventDTO> CreateEventAsync(EventDTO eventDto)
        {
            var eventEntity = _mapper.Map<EventClass>(eventDto);
            eventEntity.Category = null; // added 11:26 1-6
            var result = await _eventRepo.AddAsync(eventEntity);
            return _mapper.Map<EventDTO>(result);
        }

        public async Task UpdateEventAsync(int id, EventDTO eventDto)
        {
            var existingEvent = await _eventRepo.GetByIdAsync(id);
            if (existingEvent != null)
            {
                _mapper.Map(eventDto, existingEvent);
                await _eventRepo.UpdateAsync(existingEvent);
            }
        }

        public async Task DeleteEventAsync(int id)
        {
            await _eventRepo.DeleteAsync(id);
        }
    }
}
