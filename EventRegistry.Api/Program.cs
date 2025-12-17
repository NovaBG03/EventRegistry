using EventRegistry.Api.Authentication;
using EventRegistry.Business.Extensions;
using EventRegistry.Data.Extensions;
using EventRegistry.Messaging.Extensions;
using Scalar.AspNetCore;

namespace EventRegistry.Api;

internal abstract class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddOpenApi();

        builder.Services.AddControllers();

        builder.Services.AddBusinessServices();
        builder.Services.AddInMemoryRepositories();

        builder.Services.AddServiceBusMessaging(
            builder.Configuration.GetConnectionString("ServiceBus") ??
            throw new InvalidOperationException("Connection string 'ServiceBus' is not configured.")
        );

        builder.Services
            .AddAuthentication(ApiKeyAuthenticationExtensions.SchemeName)
            .AddApiKeyAuthentication();
        builder.Services.AddAuthorization();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference("/");
        }

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
