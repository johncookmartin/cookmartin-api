using CookMartin.Data.Interfaces;

namespace CookMartin.Data.SqlAccess;

public class SqlTransactionRunner : ITransactionRunner
{
    private readonly IUnitOfWorkFactory _uowFactory;

    public SqlTransactionRunner(IUnitOfWorkFactory uowFactory)
    {
        _uowFactory = uowFactory;
    }

    public async Task ExecuteAsync(Func<IWriteDb, Task> action)
    {
        using var uow = _uowFactory.Create();
        var writeDb = new SqlWriteDb(uow);

        try
        {
            await action(writeDb);
            uow.Commit();
        }
        catch
        {
            uow.Rollback();
            throw;
        }
    }

    public async Task<T> ExecuteAsync<T>(Func<IWriteDb, Task<T>> action)
    {
        using var uow = _uowFactory.Create();
        var writeDb = new SqlWriteDb(uow);

        try
        {
            var result = await action(writeDb);
            uow.Commit();
            return result;

        }
        catch
        {
            uow.Rollback();
            throw;
        }
    }
}
