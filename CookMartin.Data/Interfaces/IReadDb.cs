using System.Data;

namespace CookMartin.Data.Interfaces;

public interface IReadDb
{
    Task<IEnumerable<T>> QueryAsync<T, U>(string queryString, U parameters, CommandType commandType = CommandType.StoredProcedure);
    Task<T?> QuerySingleOrDefaultAsync<T, U>(string queryString, U parameters, CommandType commandType = CommandType.StoredProcedure);
    Task<T> QuerySingleAsync<T, U>(string queryString, U parameters, CommandType commandType = CommandType.StoredProcedure);
}