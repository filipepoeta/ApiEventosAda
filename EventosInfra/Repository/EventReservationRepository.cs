using Dapper;
using EventosServices.Entity;
using EventosServices.Interfaces;
using System.Data;
using System.Data.Common;

namespace EventosInfra.Repository;

public class EventReservationRepository : IEventReservationRepository
{
    private readonly IDbConnection _connection;

    public EventReservationRepository(IDbConnection connection)
    {
        _connection = connection;
    }
    public async Task<bool> AddReservationAsync(EventReservation reservation, int idEvent)
    {
        string query = "INSERT INTO EventReservation (IdEvent,PersonName,Quantity) VALUES (@IdEvent,@PersonName,@Quantity)";
        DynamicParameters parametro = new(reservation);
        parametro.Add("IdEvent", idEvent);
        var result = await _connection.ExecuteAsync(query,parametro);
        return result > 0;
    }

    public async Task<bool> UpdateQuantityReservationAsync(int id, int quantity)
    {
        string query = "UPDATE EventReservation SET quantity = @quantity where idReservation = @id";
        DynamicParameters parametro = new();
        parametro.Add("id", id);
        parametro.Add("quantity", quantity);
        var result = await _connection.ExecuteAsync(query, parametro);
        return result > 0;

    }
    public async Task<bool> RemoveReservationAsync(int id)
    {
        string query = "DELETE FROM EventReservation where idReservation = @id";
        DynamicParameters parametro = new();
        parametro.Add("id", id);
        var result = await _connection.ExecuteAsync(query, parametro);
        return result > 0;
    }
    public async Task<IEnumerable<EventReservation>> ConsultReservationPersonNameAndTitleAsync(string name, string title)
    {
        string query = "SELECT * FROM EventReservation INNER JOIN CityEvent ON CityEvent.IdEvent = EventReservation.IdEvent WHERE PersonName = @name AND Title LIKE @title;";
        title = $"%{title}%";
        DynamicParameters parametros = new();
        parametros.Add("name", name);
        parametros.Add("title", title);
        var result = await _connection.QueryAsync<EventReservation>(query, parametros);
        return result.ToList();
    }

    public async Task<EventReservation> BuscarId(int id)
    {
        string query = "SELECT * FROM EventReservation where idReservation = @id";
        DynamicParameters parametro = new();
        parametro.Add("id", id);   
        var result = await _connection.QueryFirstOrDefaultAsync<EventReservation>(query,parametro);
        return result;
    }


}
