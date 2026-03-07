using System.Data;

namespace CookMartin.Data.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IDbConnection Connection { get; }
    IDbTransaction Transaction { get; }

    void Commit();
    void Rollback();
}