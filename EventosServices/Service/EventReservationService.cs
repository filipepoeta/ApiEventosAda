using AutoMapper;
using EventosServices.Dto;
using EventosServices.Entity;
using EventosServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventosServices.Service;


public class EventReservationService : IEventReservationService
{
    private readonly IEventReservationRepository _repository;
    private readonly IMapper _mapper;
    public EventReservationService(IEventReservationRepository repository, IMapper mapper)
    {
        _repository= repository;
        _mapper= mapper;

    }
    public async Task<bool> AddReservationAsync(EventReservationDto reservation, int idEvent)
    {
        EventReservation eventReservation = _mapper.Map<EventReservation>(reservation);
        return await _repository.AddReservationAsync(eventReservation,idEvent);
    }

    public async Task<bool> RemoveReservationAsync(int id)
    {
        
        return await _repository.RemoveReservationAsync(id);

    }
    public async Task<bool> UpdateQuantityReservationAsync(int id, int quantity)
    {
        
        return await _repository.UpdateQuantityReservationAsync(id, quantity);

    }
    public async Task<IEnumerable<EventReservation>> ConsultReservationPersonNameAndTitleAsync(string name, string title)
    {
        var eventReservation =  await _repository.ConsultReservationPersonNameAndTitleAsync(name, title);

        if (eventReservation == null) throw new Exception("Reserva não encontrada em nossa base de dados");

        return eventReservation.ToList();
    }


}
