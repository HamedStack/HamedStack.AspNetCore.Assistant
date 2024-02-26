// ReSharper disable IdentifierTypo
// ReSharper disable CommentTypo

using Microsoft.AspNetCore.Routing;

namespace HamedStack.AspNetCore.Assistant.Abstractions;

/// <summary>
/// Defines the contract for a minimal API endpoint in the context of the REPR (Request-Endpoint-Response) Pattern.
/// </summary>
/// <remarks>
/// Implementations of this interface are responsible for defining how an endpoint should be handled, including the setup of request delegates and the mapping of routes. This interface facilitates a clear and consistent approach to defining minimal API endpoints.
/// </remarks>
public interface IMinimalApiEndpoint
{
    /// <summary>
    /// Handles the setup and configuration of the endpoint.
    /// </summary>
    /// <param name="endpoint">The <see cref="IEndpointRouteBuilder"/> used to configure the endpoint's routing.</param>
    /// <remarks>
    /// Implementations should use this method to define the endpoint's behavior, including the mapping of request paths, the specification of HTTP methods, and the registration of request handling delegates. This method is called during application startup to configure the endpoint.
    /// </remarks>
    void HandleEndpoint(IEndpointRouteBuilder endpoint);
}