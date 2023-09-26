// ReSharper disable UnusedMember.Global
// ReSharper disable CheckNamespace
// ReSharper disable UnusedType.Global

using Microsoft.AspNetCore.Http;

namespace HamedStack.AspNetCore.Assistant.Extensions.HttpRequestExtended;

/// <summary>
/// Provides extension methods for the <see cref="HttpRequest"/> class.
/// </summary>
public static class HttpRequestExtensions
{
    /// <summary>
    /// Determines if the request is an AJAX request.
    /// </summary>
    /// <param name="request">The HTTP request.</param>
    /// <returns>True if the request is an AJAX request, otherwise false.</returns>
    public static bool IsAjaxRequest(this HttpRequest request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        return request.Headers["X-Requested-With"] == "XMLHttpRequest";
    }

    /// <summary>
    /// Constructs and returns the full URL from the provided request.
    /// </summary>
    /// <param name="request">The HTTP request.</param>
    /// <returns>The constructed URL from the request.</returns>
    public static string GetUrl(this HttpRequest request)
    {
        return $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}";
    }
}