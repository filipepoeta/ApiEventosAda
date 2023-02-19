using EventosServices.Entity;

namespace EventosServices.Interfaces;

public interface IEventReservationRepository
{
    Task<bool> AddReservationAsync(EventReservation reservation, int id);
    Task<bool> UpdateQuantityReservationAsync(int id, int quantity);
    Task<bool> RemoveReservationAsync(int id);
    Task<IEnumerable<EventReservation>> ConsultReservationPersonNameAndTitleAsync(string name, string title);
    Task<EventReservation> BuscarId(int id);
}
