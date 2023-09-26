// ReSharper disable UnusedMember.Global

namespace HamedStack.AspNetCore.Assistant.Implementations;

/// <summary>
/// Represents information about a host, capturing details such as connection ID, IP addresses, and ports.
/// </summary>
public class HostInfo
{
    /// <summary>
    /// Gets or sets the unique connection identifier for the host.
    /// </summary>
    /// <value>The connection ID of the host.</value>
    public string? ConnectionId { get; set; }

    /// <summary>
    /// Gets or sets the IP address of the remote host.
    /// </summary>
    /// <value>The remote IP address.</value>
    public string? RemoteIp { get; set; }

    /// <summary>
    /// Gets or sets the IP address of the local host.
    /// </summary>
    /// <value>The local IP address.</value>
    public string? LocalIp { get; set; }

    /// <summary>
    /// Gets or sets the port number of the remote host.
    /// </summary>
    /// <value>The remote port number.</value>
    public int? RemotePort { get; set; }

    /// <summary>
    /// Gets or sets the port number of the local host.
    /// </summary>
    /// <value>The local port number.</value>
    public int? LocalPort { get; set; }
}
