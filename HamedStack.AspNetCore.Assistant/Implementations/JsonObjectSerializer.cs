// ReSharper disable UnusedMember.Global

using System.Text.Json;
using HamedStack.AspNetCore.Assistant.Abstractions;
using Microsoft.Extensions.Logging;

namespace HamedStack.AspNetCore.Assistant.Implementations;

/// <summary>
/// Provides methods to serialize and deserialize objects to and from JSON strings 
/// using the System.Text.Json library, with logging capabilities.
/// </summary>
public class JsonObjectSerializer : IObjectSerializer
{
    private readonly ILogger<JsonObjectSerializer> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonObjectSerializer"/> class.
    /// </summary>
    /// <param name="logger">The logger to be used for logging serialization and deserialization events.</param>
    public JsonObjectSerializer(ILogger<JsonObjectSerializer> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Serializes the specified object to a JSON string.
    /// </summary>
    /// <typeparam name="T">The type of the object to serialize.</typeparam>
    /// <param name="obj">The object to serialize.</param>
    /// <param name="options">Optional settings for JSON serialization. If null, default settings are used.</param>
    /// <returns>A JSON string representation of the object.</returns>
    /// <exception cref="Exception">Throws an exception if serialization fails.</exception>
    public string Serialize<T>(T obj, JsonSerializerOptions? options = null)
    {
        try
        {
            return JsonSerializer.Serialize(obj, options);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Serialization failed for object of type {ObjectType}", typeof(T).FullName);
            throw;
        }
    }

    /// <summary>
    /// Deserializes the specified JSON string to an object of type T.
    /// </summary>
    /// <typeparam name="T">The type of the object to deserialize.</typeparam>
    /// <param name="data">The JSON string to deserialize.</param>
    /// <param name="options">Optional settings for JSON deserialization. If null, default settings are used.</param>
    /// <returns>An object of type T or null if deserialization fails.</returns>
    public T? Deserialize<T>(string data, JsonSerializerOptions? options = null)
    {
        try
        {
            return JsonSerializer.Deserialize<T>(data, options);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Deserialization failed for JSON data: {Data}", data);
            return default;
        }
    }
}

