using CookMartin.Data.Interfaces;
using System.Data;

namespace CookMartin.Data.SqlAccess;

public class SqlUnitOfWork : IUnitOfWork
{
    private bool _completed;
    public IDbConnection Connection { get; }
    public IDbTransaction Transaction { get; }
    public SqlUnitOfWork(IConnectionFactory connectionFactory)
    {
        Connection = connectionFactory.CreateConnection();
        Connection.Open();
        Transaction = Connection.BeginTransaction();
    }

    public void Commit()
    {
        if (_completed)
        {
            throw new InvalidOperationException("This unit of work has already been completed.");
        }

        Transaction.Commit();
        _completed = true;
    }
    public void Rollback()
    {
        if (_completed)
        {
            return;
        }

        Transaction.Rollback();
        _completed = true;
    }

    public void Dispose()
    {
        if (!_completed)
        {
            try
            {
                Transaction.Rollback();
            }
            catch
            {
                // Ignore exceptions during rollback to ensure resources are cleaned up
            }
        }

        Transaction?.Dispose();
        Connection?.Dispose();
    }
}
