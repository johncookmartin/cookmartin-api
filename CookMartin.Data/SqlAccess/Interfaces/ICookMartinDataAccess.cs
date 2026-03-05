using System.Data;

namespace CookMartin.Data.SqlAccess.Interfaces;

public interface ICookMartinDataAccess
{
    Task ExecuteDataAsync<T>(string queryString, T parameters, CommandType commandType = CommandType.StoredProcedure);
    Task<IEnumerable<T>> QueryDataAsync<T, U>(string queryString, U parameters, CommandType commandType = CommandType.StoredProcedure);
}