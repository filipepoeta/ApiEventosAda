using EventosApi.Filter;
using EventosServices.Dto;
using EventosServices.Entity;
using EventosServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Threading.Tasks;

namespace EventosApi.Controllers
{
   
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class CityEventController : ControllerBase
    {
        private readonly ICityEventService _cityEventService;
        public CityEventController(ICityEventService cityEventService)
        {
            _cityEventService= cityEventService;
        }

        [HttpGet("title")]
        [TypeFilter(typeof(ExcecaoGeralFilter))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CityEvent>>> GetEventTitle(string title)
        {
            IEnumerable<CityEvent> eventos = await _cityEventService.ConsultEventTitleAsync(title);
            if (eventos == null) return BadRequest();
            return Ok(eventos);
        }

        [HttpGet("eventPrice")]
        [TypeFilter(typeof(ExcecaoGeralFilter))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CityEvent>>> GetEventPrice(decimal minPrice, decimal maxPrice, DateTime date)
        {
            IEnumerable<CityEvent> eventos = await _cityEventService.ConsultEventRangePriceAndDataAsync(minPrice, maxPrice, date);
            return Ok(eventos);
        }

        [HttpGet("localEvent")]
        [TypeFilter(typeof(ExcecaoGeralFilter))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CityEvent>>> GetEventLocalAndData(string local, DateTime date)
        {
            IEnumerable<CityEvent> eventos = await _cityEventService.ConsultEventLocalAndDataAsync(local, date);
            return Ok(eventos);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [TypeFilter(typeof(ExcecaoGeralFilter))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> PostEvent(CityEventDto cityEvent)
        {
            if (!await _cityEventService.AddEventAsync(cityEvent)) return BadRequest();
            return CreatedAtAction(nameof(GetEventTitle), cityEvent);
        }

        [HttpPut("update")]
        [Authorize(Roles = "admin")]
        [TypeFilter(typeof(ExcecaoGeralFilter))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateEvent(int id, CityEventDto cityEvent)
        {
            if(!await _cityEventService.UpdateEventAsync(id, cityEvent)) return BadRequest();
            return NoContent();
        }

        [HttpDelete]
        [Authorize(Roles = "admin")]
        [TypeFilter(typeof(ExcecaoGeralFilter))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteEvent( int id)
        {
            if (!await _cityEventService.RemoveEventAsync(id)) return BadRequest();
            return NoContent();
        }
     
    }
}
