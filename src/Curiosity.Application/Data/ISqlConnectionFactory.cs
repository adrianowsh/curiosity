using System.Data;

namespace Curiosity.Application.Data;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}
