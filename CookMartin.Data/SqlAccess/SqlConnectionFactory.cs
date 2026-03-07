using CookMartin.Data.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace CookMartin.Data.SqlAccess;

public class SqlConnectionFactory : IConnectionFactory
{
    private readonly IConfiguration _config;

    public SqlConnectionFactory(IConfiguration config)
    {
        _config = config;
    }

    public IDbConnection CreateConnection(string connectionStringName)
    {
        var connectionString = _config.GetConnectionString(connectionStringName);

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException($"Connection string '{connectionStringName}' not found.");
        }

        return new SqlConnection(connectionString);
    }
}
