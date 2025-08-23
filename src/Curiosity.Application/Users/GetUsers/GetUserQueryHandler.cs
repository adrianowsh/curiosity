using System.Data;
using Curiosity.Application.Abstractions.Messaging;
using Curiosity.Application.Authentication;
using Curiosity.Application.Data;
using Curiosity.Domain.Abstractions;
using Dapper;

namespace Curiosity.Application.Users.GetUsers;

internal sealed class GetUserQueryHandler(
    ISqlConnectionFactory sqlConnectionFactory,
    IUserContext userContext) : IQueryHandler<GetUserQuery, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection connection = sqlConnectionFactory.CreateConnection();

        const string sql = """
                           SELECT 
                               u.id AS Id, 
                               u.email AS Email, 
                               u.first_name AS FirstName, 
                               u.last_name AS LastName
                           FROM users u
                           WHERE u.IDENTITY_ID = @IdentityId
                           """;

        UserResponse user = await connection.QuerySingleAsync<UserResponse>
            (sql,
            new
            {
                IdentityId = userContext.IdentityId
            });

        return user;
    }
}
