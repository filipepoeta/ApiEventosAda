using EventosServices.Entity;

namespace EventosServices.Interfaces;

public interface ICityEventRepository
{
    Task<bool> AddEventAsync(CityEvent cityEvent);
    Task<bool> UpdateEventAsync(int id, CityEvent cityEvent);
    Task<bool> RemoveEventAsync(int id);
    Task<IEnumerable<CityEvent>> ConsultEventTitleAsync(string title);
    Task<IEnumerable<CityEvent>> ConsultEventLocalAndDataAsync(string local, DateTime date);
    Task<IEnumerable<CityEvent>> ConsultEventRangePriceAndDataAsync(decimal minPrice,decimal maxPrice, DateTime date);
    Task<bool> InativEvent(int id);
    Task<bool> ConsultReservationOnEvent(int id);
}
