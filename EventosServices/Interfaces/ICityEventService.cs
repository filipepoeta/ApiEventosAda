using EventosServices.Dto;
using EventosServices.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventosServices.Interfaces;

public interface ICityEventService
{
    Task<bool> AddEventAsync(CityEventDto cityEvent);
    Task<bool> UpdateEventAsync(int id, CityEventDto cityEvent);
    Task<bool> RemoveEventAsync(int id);
    Task<IEnumerable<CityEvent>> ConsultEventTitleAsync(string title);
    Task<IEnumerable<CityEvent>> ConsultEventLocalAndDataAsync(string local, DateTime date);
    Task<IEnumerable<CityEvent>> ConsultEventRangePriceAndDataAsync(decimal minPrice, decimal maxPrice, DateTime date);
}
