using EventosApi.Filter;
using EventosServices.Dto;
using EventosServices.Entity;
using EventosServices.Interfaces;
using EventosServices.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EventosApi.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class EventReservationController : ControllerBase
    {
        private readonly IEventReservationService _eventReservationService;
        public EventReservationController(IEventReservationService eventReservationService)
        {
            _eventReservationService = eventReservationService;
        }

        [HttpGet("consultar")]
        [Authorize]
        [TypeFilter(typeof(ExcecaoGeralFilter))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EventReservation>>> GetReserva(string name, string title)
        {
            IEnumerable<EventReservation> reservations = await _eventReservationService.ConsultReservationPersonNameAndTitleAsync(name, title);
            return Ok(reservations);
        }

        [HttpPost]
        [Authorize]
        [TypeFilter(typeof(ExcecaoGeralFilter))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> InsertReservation(EventReservationDto reservation, int id)
        {
            if (!await _eventReservationService.AddReservationAsync(reservation, id)) return BadRequest();
            return CreatedAtAction(nameof(GetReserva), reservation);
        }

        [HttpPut("update")]
        [Authorize(Roles = "admin")]
        [TypeFilter(typeof(ExcecaoGeralFilter))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateReserv(int id, int quantity)
        {
            var updateQuant = await _eventReservationService.UpdateQuantityReservationAsync(id, quantity);
            if (!updateQuant) return BadRequest();
            return NoContent();
        }

        [HttpDelete]
        [Authorize(Roles = "admin")]
        [TypeFilter(typeof(ExcecaoGeralFilter))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult>RemoveReserv(int id)
        {
            if (!await _eventReservationService.RemoveReservationAsync(id)) return BadRequest();
            return NoContent();
        }
    }
}
