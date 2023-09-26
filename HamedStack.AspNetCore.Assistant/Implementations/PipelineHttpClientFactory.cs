
// ReSharper disable UnusedMember.Global

using HamedStack.AspNetCore.Assistant.Abstractions;

namespace HamedStack.AspNetCore.Assistant.Implementations;

/// <summary>
/// Factory implementation for creating instances of HttpClient configured with custom handlers.
/// </summary>
public class PipelineHttpClientFactory : IPipelineHttpClientFactory
{
    /// <inheritdoc cref="IPipelineHttpClientFactory.CreateClient(DelegatingHandler[])"/>
    public HttpClient CreateClient(params DelegatingHandler[] handlers)
    {
        var handlerPipeline = CreateHandlerPipeline(handlers);
        var client = new HttpClient(handlerPipeline);
        return client;
    }

    /// <inheritdoc cref="IPipelineHttpClientFactory.CreateClient(Action{HttpClient}, DelegatingHandler[])"/>
    public HttpClient CreateClient(Action<HttpClient> config, params DelegatingHandler[] handlers)
    {
        var handlerPipeline = CreateHandlerPipeline(handlers);
        var client = new HttpClient(handlerPipeline);

        config.Invoke(client);

        return client;
    }

    /// <summary>
    /// Constructs a linked pipeline of delegating handlers ending with a HttpClientHandler.
    /// </summary>
    /// <param name="handlers">The handlers to link together.</param>
    /// <returns>The first handler in the pipeline.</returns>
    private static HttpMessageHandler CreateHandlerPipeline(DelegatingHandler[] handlers)
    {
        // If there are no handlers provided, simply return a default HttpClientHandler
        if (handlers.Length == 0)
        {
            return new HttpClientHandler();
        }

        // Iterate through all handlers, linking each one to the next in the list.
        // This forms a chain of handlers where each handler's InnerHandler property
        // points to the next handler in the sequence.
        for (var i = 0; i < handlers.Length - 1; i++)
        {
            handlers[i].InnerHandler = handlers[i + 1];
        }

        // Set the last handler's InnerHandler to a new instance of HttpClientHandler.
        // This ensures that the chain always ends with HttpClientHandler, which is the handler 
        // responsible for sending the HTTP request and receiving the HTTP response.
        handlers[^1].InnerHandler = new HttpClientHandler();

        // Return the first handler in the chain.
        // When HttpClient sends a request, it will pass through each handler in the chain 
        // in the order they were linked together.
        return handlers[0];
    }

}
