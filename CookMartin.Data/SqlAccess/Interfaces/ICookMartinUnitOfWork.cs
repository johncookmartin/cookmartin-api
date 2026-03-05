using System.Data;

namespace CookMartin.Data.SqlAccess.Interfaces;

public interface ICookMartinUnitOfWork
{
    IDbConnection Connection { get; }
    IDbTransaction Transaction { get; }

    void Commit();
    void Dispose();
    void Rollback();
}