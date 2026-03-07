using CookMartin.Data.Interfaces;
using Dapper;
using System.Data;

namespace CookMartin.Data.SqlAccess;

public class SqlReadDb : IReadDb
{
    private readonly IConnectionFactory _connectionFactory;

    public SqlReadDb(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<T>> QueryAsync<T, U>(
        string queryString,
        U parameters,
        CommandType commandType = CommandType.StoredProcedure)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<T>(queryString, parameters, commandType: commandType);
    }

    public async Task<T?> QuerySingleOrDefaultAsync<T, U>(
        string queryString,
        U parameters,
        CommandType commandType = CommandType.StoredProcedure)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<T>(queryString, parameters, commandType: commandType);
    }

    public async Task<T> QuerySingleAsync<T, U>(
        string queryString,
        U parameters,
        CommandType commandType = CommandType.StoredProcedure)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QuerySingleAsync<T>(queryString, parameters, commandType: commandType);
    }
}
