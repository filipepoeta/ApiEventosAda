using EventosServices.Dto;
using EventosServices.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventosServices.Interfaces;

public interface IEventReservationService
{
    Task<bool> AddReservationAsync(EventReservationDto reservation, int idEvent);
    Task<bool> UpdateQuantityReservationAsync(int id, int quantity);
    Task<bool> RemoveReservationAsync(int id);
    Task<IEnumerable<EventReservation>> ConsultReservationPersonNameAndTitleAsync(string name, string title);
}
