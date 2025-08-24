using System.Data;
using Curiosity.Application.Abstractions.Messaging;
using Curiosity.Application.Data;
using Curiosity.Domain.Abstractions;
using Dapper;

namespace Curiosity.Application.Users.GetUsers;

internal sealed class GetUserQueryHandler(ISqlConnectionFactory sqlConnectionFactory) 
    : IQueryHandler<GetUserQuery, IReadOnlyList<UserResponse>>
{
    public async Task<Result<IReadOnlyList<UserResponse>>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection connection = sqlConnectionFactory.CreateConnection();

        const string sql = """
                           SELECT 
                               u.id AS Id, 
                               u.email AS Email, 
                               u.name AS Name, 
                               u.status AS Status
                           FROM users u
                           """;

        IEnumerable<UserResponse> users = await connection.QueryAsync<UserResponse>
            (sql);

        return users.ToList();
    }
}
