using System.Net.Http.Json;
using Curiosity.Application.Authentication;
using Curiosity.Domain.Abstractions;
using Curiosity.Infrastructure.Authentication.Model;
using Microsoft.Extensions.Options;

namespace Curiosity.Infrastructure.Authentication;

internal sealed class JwtService(HttpClient httpClient, IOptions<KeycloakOptions> keycloakOptions)
    : IJwtService
{
    private static readonly Error AuthenticationFailed = new(
        "Keycloak.AuthenticationFailed",
        "Failed to acquire access token do to authentication failure");

    private readonly KeycloakOptions _keycloakOptions = keycloakOptions.Value;

    public async Task<Result<string>> GetAccessTokenAsync(
        string email,
        string password,
        CancellationToken cancellationToken)
    {
        try
        {
            var authRequestParameters = new KeyValuePair<string, string>[]
            {
                new("client_id", _keycloakOptions.AuthClientId),
                new("client_secret", _keycloakOptions.AuthClientSecret),
                new("scope", "openid email"),
                new("grant_type", "password"),
                new("username", email),
                new("password", password)
            };

            using var authorizationRequestContent = new FormUrlEncodedContent(authRequestParameters);

            HttpResponseMessage response = await httpClient.PostAsync(
                "",
                authorizationRequestContent,
                cancellationToken);

            response.EnsureSuccessStatusCode();

            AuthorizationToken? authorizationToken = await response
                .Content
                .ReadFromJsonAsync<AuthorizationToken>(cancellationToken);

            return authorizationToken?.AccessToken
                   ?? Result.Failure<string>(AuthenticationFailed);
        }
        catch (HttpRequestException)
        {
            return Result.Failure<string>(AuthenticationFailed);
        }
    }
}
