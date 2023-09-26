// ReSharper disable UnusedMember.Global

using System.Text.Json;

namespace HamedStack.AspNetCore.Assistant.Abstractions;

/// <summary>
/// Provides methods for object serialization and deserialization.
/// </summary>
public interface IObjectSerializer
{
    /// <summary>
    /// Serializes the specified object to a JSON string.
    /// </summary>
    /// <typeparam name="T">The type of the object to serialize.</typeparam>
    /// <param name="obj">The object to serialize.</param>
    /// <param name="options">The JSON serialization options. If null, default options are used.</param>
    /// <returns>A JSON string representation of the object.</returns>
    string Serialize<T>(T obj, JsonSerializerOptions? options = null);

    /// <summary>
    /// Deserializes the specified JSON string to an object of type T.
    /// </summary>
    /// <typeparam name="T">The type of the object to deserialize.</typeparam>
    /// <param name="data">The JSON string to deserialize.</param>
    /// <param name="options">The JSON deserialization options. If null, default options are used.</param>
    /// <returns>An object of type T or null if deserialization fails.</returns>
    T? Deserialize<T>(string data, JsonSerializerOptions? options = null);
}