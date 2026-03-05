using CookMartin.Data.SqlAccess.Interfaces;
using Dapper;
using System.Data;

namespace CookMartin.Data.SqlAccess;

public class CookMartinSqlDataAccess : ICookMartinDataAccess
{
    private readonly ICookMartinUnitOfWork _uow;

    public CookMartinSqlDataAccess(ICookMartinUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<IEnumerable<T>> QueryDataAsync<T, U>(
        string queryString,
        U parameters,
        CommandType commandType = CommandType.StoredProcedure)
    {
        return await _uow.Connection.QueryAsync<T>(
            queryString,
            parameters,
            transaction: _uow.Transaction,
            commandType: commandType);
    }

    public async Task ExecuteDataAsync<T>(
        string queryString,
        T parameters,
        CommandType commandType = CommandType.StoredProcedure)
    {
        await _uow.Connection.ExecuteAsync(
            queryString,
            parameters,
            transaction: _uow.Transaction,
            commandType: commandType);
    }
}
