using AutoMapper;
using EventosServices.Dto;
using EventosServices.Entity;
using EventosServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventosServices.Service
{
    public class CityEventService : ICityEventService
    {
        private readonly ICityEventRepository _repository;
        private readonly IMapper _mapper;

        public CityEventService(ICityEventRepository repository, IMapper mapper)
        {
            _repository= repository;
            _mapper= mapper;
        }

        public async Task<bool> AddEventAsync(CityEventDto cityEvent)
        {
            CityEvent entity = _mapper.Map<CityEvent>(cityEvent);
            return await _repository.AddEventAsync(entity);
        }

        public async Task<bool> UpdateEventAsync(int id, CityEventDto cityEvent)
        {
           
            CityEvent entity = _mapper.Map<CityEvent>(cityEvent);
            return await _repository.UpdateEventAsync(id, entity);
        }

        public async Task<bool> RemoveEventAsync(int id)
        {
            bool quantReservation = await _repository.ConsultReservationOnEvent(id);
            if (quantReservation != false)
            {
            return await _repository.RemoveEventAsync(id);
            }

            return await _repository.InativEvent(id);
        }
        public async Task<IEnumerable<CityEvent>> ConsultEventTitleAsync(string title)
        {
            return await _repository.ConsultEventTitleAsync(title);
        }
        public async Task<IEnumerable<CityEvent>> ConsultEventLocalAndDataAsync(string local, DateTime date)
        {
            return await _repository.ConsultEventLocalAndDataAsync(local, date);
        }

        public async Task<IEnumerable<CityEvent>> ConsultEventRangePriceAndDataAsync(decimal minPrice, decimal maxPrice, DateTime date)
        {
            return await _repository.ConsultEventRangePriceAndDataAsync(minPrice, maxPrice, date);
        }

     
    }
}
