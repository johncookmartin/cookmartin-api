using CookMartin.Data.Interfaces;

namespace CookMartin.Data.SqlAccess;

public class SqlUnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly IConnectionFactory _connectionFactory;
    public SqlUnitOfWorkFactory(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    public IUnitOfWork Create()
    {
        return new SqlUnitOfWork(_connectionFactory);
    }
}
