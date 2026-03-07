using CookMartin.Data.Interfaces;
using Dapper;
using System.Data;

namespace CookMartin.Data.SqlAccess;

public class SqlWriteDb : IWriteDb
{
    private readonly IUnitOfWork _uow;

    public SqlWriteDb(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<IEnumerable<T>> QueryAsync<T, U>(
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

    public async Task ExecuteAsync<T>(
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
