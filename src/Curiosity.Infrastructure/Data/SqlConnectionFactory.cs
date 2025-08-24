using System.Data;
using Curiosity.Application.Data;
using Npgsql;

namespace Curiosity.Infrastructure.Data;

internal sealed class SqlConnectionFactory(string connectionString) : ISqlConnectionFactory
{
    public IDbConnection CreateConnection()
    {
        var connection = new NpgsqlConnection(connectionString);

        connection.Open();

        return connection;
    }
}
