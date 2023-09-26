namespace HamedStack.AspNetCore.Assistant.Abstractions;

/// <summary>
/// Defines a factory for creating instances of HttpClient configured with custom handlers.
/// </summary>
public interface IPipelineHttpClientFactory
{
    /// <summary>
    /// Creates an HttpClient configured with the provided handlers.
    /// </summary>
    /// <param name="handlers">An array of delegating handlers to configure for the HttpClient.</param>
    /// <returns>An instance of HttpClient configured with the provided handlers.</returns>
    /// <example>
    /// This sample shows how to call the <see cref="CreateClient(DelegatingHandler[])"/> method.
    /// <code>
    /// var factory = new PipelineHttpClientFactory();
    /// var client = factory.CreateClient(new LoggingHandler(), new RetryHandler());
    /// </code>
    /// </example>
    HttpClient CreateClient(params DelegatingHandler[] handlers);

    /// <summary>
    /// Creates an HttpClient configured with the provided handlers and custom configuration.
    /// </summary>
    /// <param name="config">A delegate to configure the HttpClient instance.</param>
    /// <param name="handlers">An array of delegating handlers to configure for the HttpClient.</param>
    /// <returns>An instance of HttpClient configured with the provided handlers.</returns>
    /// <example>
    /// This sample shows how to call the <see cref="CreateClient(Action{HttpClient}, DelegatingHandler[])"/> method.
    /// <code>
    /// var factory = new PipelineHttpClientFactory();
    /// var client = factory.CreateClient(httpClient =>
    /// {
    ///     httpClient.BaseAddress = new Uri("https://api.example.com");
    ///     httpClient.DefaultRequestHeaders.Add("Custom-Header", "HeaderValue");
    /// },
    /// new LoggingHandler(), new RetryHandler());
    /// </code>
    /// </example>
    HttpClient CreateClient(Action<HttpClient> config, params DelegatingHandler[] handlers);
}