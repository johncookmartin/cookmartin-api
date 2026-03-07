using System.Data;

namespace CookMartin.Data.Interfaces;

public interface IWriteDb
{
    Task ExecuteAsync<T>(string queryString, T parameters, CommandType commandType = CommandType.StoredProcedure);
    Task<IEnumerable<T>> QueryAsync<T, U>(string queryString, U parameters, CommandType commandType = CommandType.StoredProcedure);
}