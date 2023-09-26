// ReSharper disable UnusedMember.Global
// ReSharper disable CheckNamespace
// ReSharper disable UnusedType.Global

using System.Text;
using Microsoft.AspNetCore.Http;

namespace HamedStack.AspNetCore.Assistant.Extensions.HttpResponseExtended;

/// <summary>
/// Provides extension methods for the <see cref="HttpResponse"/> class.
/// </summary>
public static class HttpResponseExtensions
{
    /// <summary>
    /// Reads the response body as a string without disrupting the response stream.
    /// </summary>
    /// <param name="response">The HttpResponse object.</param>
    /// <returns>The response body as a string.</returns>
    public static async Task<string> ReadBodyAsStringAsync(this HttpResponse response)
    {
        response.Body.Seek(0, SeekOrigin.Begin);
        var reader = new StreamReader(response.Body, Encoding.UTF8);
        var body = await reader.ReadToEndAsync();
        response.Body.Seek(0, SeekOrigin.Begin);
        return body;
    }
}