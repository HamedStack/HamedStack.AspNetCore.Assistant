// ReSharper disable CheckNamespace
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global

using System.Diagnostics;
using HamedStack.AspNetCore.Assistant.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace HamedStack.AspNetCore.Assistant.Extensions.HttpContextExtended;

/// <summary>
/// Provides extension methods for the <see cref="HttpContext"/> class.
/// </summary>
public static class HttpContextExtensions
{
    /// <summary>
    /// Retrieves the unique request ID from the current HTTP context or from the current Activity trace.
    /// </summary>
    /// <param name="httpContext">The current HttpContext instance.</param>
    /// <returns>The unique request ID.</returns>
    public static string GetRequestId(this HttpContext httpContext)
    {
        return Activity.Current?.Id ?? httpContext.TraceIdentifier;
    }

    /// <summary>
    /// Gets the connection-related features of the current HTTP context.
    /// </summary>
    /// <param name="context">The current HttpContext instance.</param>
    /// <returns>An object representing connection-related features, or null if not available.</returns>
    public static IHttpConnectionFeature? GetHttpConnectionFeature(this HttpContext context)
    {
        return context.Features.Get<IHttpConnectionFeature>();
    }
    
    /// <summary>
    /// Retrieves information about the host (IP addresses, ports, connection ID) that sent the request.
    /// </summary>
    /// <param name="context">The current HttpContext instance.</param>
    /// <returns>A <see cref="HostInfo"/> object containing details about the caller's host.</returns>
    public static HostInfo GetCallerHost(this HttpContext context)
    {
        var callerFeatures = context.Features.Get<IHttpConnectionFeature>();
        var callerHostRemoteIp = callerFeatures?.RemoteIpAddress?.ToString();
        var callerHostRemotePort = callerFeatures?.RemotePort;
        var callerHostConnectionId = callerFeatures?.ConnectionId;
        var callerHostLocalIp = callerFeatures?.LocalIpAddress?.ToString();
        var callerHostLocalPort = callerFeatures?.LocalPort;

        return new HostInfo
        {
            ConnectionId = callerHostConnectionId,
            RemoteIp = callerHostRemoteIp,
            LocalIp = callerHostLocalIp,
            RemotePort = callerHostRemotePort,
            LocalPort = callerHostLocalPort
        };
    }
}