// ReSharper disable IdentifierTypo

using HamedStack.AspNetCore.Assistant.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace HamedStack.AspNetCore.Assistant.Extensions;

/// <summary>
/// Provides extension methods for <see cref="IServiceCollection"/> and <see cref="WebApplication"/> to facilitate the registration and usage of minimal API endpoints following the REPR (Request-Endpoint-Response) Pattern.
/// </summary>
public static class MinimalApiEndpointsExtensions
{
    /// <summary>
    /// Adds minimal API endpoints to the <see cref="IServiceCollection"/>, discovering and registering implementations of <see cref="IMinimalApiEndpoint"/> as transient services.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the endpoints to.</param>
    /// <returns>The <see cref="IServiceCollection"/> for chaining.</returns>
    /// <remarks>
    /// This method scans the application's assemblies to find all types that implement <see cref="IMinimalApiEndpoint"/>, are not abstract, and are not interfaces, and then registers them as transient services. This allows for automatic discovery and registration of endpoints, facilitating a plug-and-play approach for adding new API endpoints.
    /// </remarks>
    public static IServiceCollection AddMinimalApiEndpoints(this IServiceCollection services)
    {
        var endpointTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => typeof(IMinimalApiEndpoint).IsAssignableFrom(type) && type is { IsInterface: false, IsAbstract: false });

        foreach (var type in endpointTypes)
        {
            services.AddTransient(typeof(IMinimalApiEndpoint), type);
        }

        return services;
    }

    /// <summary>
    /// Configures the <see cref="WebApplication"/> to use the minimal API endpoints registered in the service collection.
    /// </summary>
    /// <param name="app">The <see cref="WebApplication"/> to configure.</param>
    /// <returns>The <see cref="WebApplication"/> for chaining.</returns>
    /// <remarks>
    /// This method retrieves all registered implementations of <see cref="IMinimalApiEndpoint"/> from the application's service provider and invokes their <see cref="IMinimalApiEndpoint.HandleEndpoint"/> method to set up the endpoints. This pattern supports a clean separation of concerns and promotes a modular approach to API development.
    /// </remarks>
    public static WebApplication UseMinimalApiEndpoints(this WebApplication app)
    {
        var endpoints = app.Services.GetServices<IMinimalApiEndpoint>();
        foreach (var endpoint in endpoints)
        {
            endpoint.HandleEndpoint(app);
        }

        return app;
    }
}