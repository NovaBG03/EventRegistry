using System.Security.Claims;
using System.Text.Encodings.Web;
using EventRegistry.Business.Security.Contracts;
using EventRegistry.Data.Repositories.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace EventRegistry.Api.Authentication;

public class ApiKeyAuthenticationHandler(
    IOptionsMonitor<ApiKeyAuthenticationOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    IApplicationRepository applicationRepository,
    ISecretHasher secretHasher)
    : AuthenticationHandler<ApiKeyAuthenticationOptions>(options, logger, encoder)
{
    private const string ApiKeyHeaderName = "X-Api-Key";

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue(ApiKeyHeaderName, out var apiKeyHeaderValues))
        {
            return AuthenticateResult.Fail("API Key header not found");
        }

        var apiKey = apiKeyHeaderValues.FirstOrDefault();

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            return AuthenticateResult.Fail("API Key header is empty");
        }

        // todo make getting the application by ID and then compare the api key
        var apiKeyHash = secretHasher.Hash(apiKey);
        var application = await applicationRepository.GetByApiKeyAsync(apiKeyHash);

        if (application == null)
        {
            return AuthenticateResult.Fail("Invalid API Key");
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, application.Id.ToString()),
            new Claim(ClaimTypes.Name, application.Name),
            new Claim("ApplicationId", application.Id.ToString())
        };

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }
}

public class ApiKeyAuthenticationOptions : AuthenticationSchemeOptions
{
}

public static class ApiKeyAuthenticationExtensions
{
    public const string SchemeName = "ApiKey";

    public static AuthenticationBuilder AddApiKeyAuthentication(this AuthenticationBuilder builder)
    {
        return builder.AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>(
            SchemeName,
            _ => { });
    }
}
