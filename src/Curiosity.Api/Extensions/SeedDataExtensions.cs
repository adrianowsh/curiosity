using System.Data;
using Curiosity.Application.Data;
using Dapper;

namespace Curiosity.Api.Extensions;

public static class SeedDataExtensions
{
    public static void SeedData(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        ISqlConnectionFactory sqlConnectionFactory = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();
        using IDbConnection connection = sqlConnectionFactory.CreateConnection();

        InsertUsers(connection);


    }

    public static void InsertUsers(IDbConnection connection)
    {
        const string checkSql = "SELECT COUNT(*) FROM public.users;";

        int existingRecords = connection.ExecuteScalar<int>(checkSql);

        if (existingRecords > 0)
        {
            return;
        }

        List<object> users = [];
        for (int i = 0; i < 3; i++)
        {
            users.Add(new
            {
                Id = $"usr_{Guid.CreateVersion7()}",
                Name = $"user_{i}",
                Email = $"user_{i}@email.com",
                Status = true,
                InsertedAt = DateTime.UtcNow,
            });
        }

        const string sql = """
                           INSERT INTO public.users
                           (id, "name", email, status, InsertedAt VALUES(@Id, @Name, @Email, @Status, @InsertedAt);
                           """;

        connection.Execute(sql, users);
    }
}
