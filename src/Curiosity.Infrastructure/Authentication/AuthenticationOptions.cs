namespace Curiosity.Infrastructure.Authentication;

public sealed class AuthenticationOptions
{
    public string Audience { get; init; }
    public string MetadataUrl { get; init; }
    public bool RequireHttpsMetadata { get; init; }
    public string Issuer { get; init; }
}
