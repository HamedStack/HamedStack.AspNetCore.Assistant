// ReSharper disable UnusedMember.Global
// ReSharper disable CheckNamespace
// ReSharper disable UnusedType.Global

using System.Net.Http.Headers;
using System.Text.Json;

namespace HamedStack.AspNetCore.Assistant.Extensions.HttpClientExtended;

/// <summary>
/// Provides extension methods for the <see cref="HttpClient"/> class.
/// </summary>
public static class HttpClientExtensions
{
    /// <summary>
    /// Sends a POST request to the specified URL with the provided data serialized as JSON.
    /// </summary>
    /// <typeparam name="T">The type of the data object.</typeparam>
    /// <param name="httpClient">The HttpClient instance.</param>
    /// <param name="url">The request URL.</param>
    /// <param name="data">The data object to be serialized and sent as the request body.</param>
    /// <returns>The HTTP response message that represents the result of the HTTP request.</returns>
    public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient httpClient, string url, T data)
    {
        var dataAsString = JsonSerializer.Serialize(data);
        var content = new StringContent(dataAsString);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        return httpClient.PostAsync(url, content);
    }

    /// <summary>
    /// Reads the content as a string, then deserializes the JSON content to an object of type T.
    /// </summary>
    /// <typeparam name="T">The type to deserialize the content into.</typeparam>
    /// <param name="content">The HTTP content to read.</param>
    /// <returns>The deserialized object from the JSON content.</returns>
    public static async Task<T?> ReadAsJsonAsync<T>(this HttpContent content)
    {
        var dataAsString = await content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(dataAsString);
    }
}