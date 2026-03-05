using CookMartin.Data.SqlAccess.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace CookMartin.Data.SqlAccess;

public class CookMartinSqlUnitOfWork : ICookMartinUnitOfWork
{
    private const string CONNECTION_STRING_NAME = "Default";
    public IDbConnection Connection { get; }
    public IDbTransaction Transaction { get; }
    public CookMartinSqlUnitOfWork(IConfiguration config, string connectionString = CONNECTION_STRING_NAME)
    {
        Connection = new SqlConnection(config.GetConnectionString(connectionString));
        Connection.Open();
        Transaction = Connection.BeginTransaction();
    }

    public void Commit()
    {
        Transaction.Commit();
    }
    public void Rollback()
    {
        Transaction.Rollback();
    }

    public void Dispose()
    {
        Transaction?.Dispose();
        Connection?.Dispose();
    }
}
