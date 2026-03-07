using System.Data;

namespace CookMartin.Data.Interfaces;

public interface IConnectionFactory
{
    private const string CONNECTION_STRING_NAME = "Default";
    IDbConnection CreateConnection(string connectionStringName = CONNECTION_STRING_NAME);
}