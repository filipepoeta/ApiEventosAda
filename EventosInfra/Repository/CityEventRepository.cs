using Dapper;
using EventosServices.Entity;
using EventosServices.Interfaces;
using System.Data;

namespace EventosInfra.Repository;

public class CityEventRepository : ICityEventRepository
{
    private readonly IDbConnection _connection;
    public CityEventRepository(IDbConnection connection)
    {
        _connection = connection;
    }
    public async Task<bool> AddEventAsync(CityEvent cityEvent)
    {
        string query = @"INSERT INTO CityEvent(title,description, dateHourEvent, local, address, price,status) 
             VALUES (@title, @description, @dateHourEvent, @local, @address, @price, true)";

        DynamicParameters paramentros = new(cityEvent);

        var result = await _connection.ExecuteAsync(query,paramentros);
        return result > 0;
    }

    public async Task<bool> UpdateEventAsync(int id, CityEvent cityEvent)
    {
        string query = "UPDATE CityEvent set title=@title,description=@description, " +
                "dateHourEvent=@dateHourEvent, local=@local, address=@address, price=@price where idEvent=@id";
        DynamicParameters parametros = new(cityEvent);
        parametros.Add("id", id);
        var result = await _connection.ExecuteAsync(query, parametros);
        return result > 0;
       
    }
    public async Task<bool> RemoveEventAsync(int id)
    {
        string query = "DELETE FROM CityEvent where idEvent = @id";
        DynamicParameters parametros = new();
        parametros.Add("id", id);

        var result = await _connection.ExecuteAsync(query,parametros);
        return result > 0;
    }
    public async Task<IEnumerable<CityEvent>> ConsultEventTitleAsync(string title)
    {
        string query = "SELECT * FROM CityEvent where title like @title";
        title = $"%{title}%";
        DynamicParameters parametros = new(title);
        parametros.Add("title", title);

        var result = await _connection.QueryAsync<CityEvent>(query, parametros);
        return result.ToList();
    }
    public async Task<IEnumerable<CityEvent>> ConsultEventLocalAndDataAsync(string local, DateTime date)
    {
        string query = "SELECT * FROM CityEvent where Local = @local and Date(dateHourEvent) = @date;";
        DynamicParameters parametros = new();
        parametros.Add("local", local);
        parametros.Add("date", date);

        var result = await _connection.QueryAsync<CityEvent>(query, parametros);
        return result.ToList();
    }

    public async Task<IEnumerable<CityEvent>> ConsultEventRangePriceAndDataAsync(decimal minPrice, decimal maxPrice, DateTime date)
    {
        string query = "SELECT * FROM CityEvent where Date(dateHourEvent) = @date and price between @minPrice and @maxPrice";
        DynamicParameters parametros = new();
        parametros.Add("date", date);
        parametros.Add("minPrice", minPrice);
        parametros.Add("maxPrice", maxPrice);
        var result = await _connection.QueryAsync<CityEvent>(query, parametros);
        return result.ToList();
    }

    public async Task<bool> InativEvent(int id)
    {
        string query = "UPDATE CityEvent set status = false Where IdEvent = @id";
        DynamicParameters parametros = new();
        parametros.Add("id", id);
        var result = await _connection.ExecuteAsync(query, parametros);
        return result > 0;
    }

   public async Task<bool> ConsultReservationOnEvent(int id)
    {
        string query = "SELECT * FROM EventReservation WHERE idEvent = @id";
        DynamicParameters parametros = new();
        parametros.Add("id", id);
        
        return await _connection.QueryFirstOrDefaultAsync(query, parametros) == null;
    }





}
