// ReSharper disable UnusedMember.Global
// ReSharper disable CheckNamespace
// ReSharper disable UnusedType.Global

using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace HamedStack.AspNetCore.Assistant.Extensions.TempDataDictionaryExtended;

/// <summary>
/// Provides extension methods for the <see cref="ITempDataDictionary"/> interface.
/// </summary>
public static class TempDataDictionaryExtensions
{
    /// <summary>
    /// Gets and deserializes an object of type T from TempData using the specified key.
    /// </summary>
    /// <typeparam name="T">The type to deserialize the TempData value into.</typeparam>
    /// <param name="tempData">The ITempDataDictionary instance.</param>
    /// <param name="key">The key associated with the desired TempData value.</param>
    /// <returns>The deserialized object of type T, or null if not found.</returns>
    public static T? Get<T>(this ITempDataDictionary tempData, string key) where T : class
    {
        tempData.TryGetValue(key, out var o);
        return o == null ? null : JsonSerializer.Deserialize<T>((string)o);
    }

    /// <summary>
    /// Peeks and deserializes an object of type T from TempData without marking it for deletion.
    /// </summary>
    /// <typeparam name="T">The type to deserialize the TempData value into.</typeparam>
    /// <param name="tempData">The ITempDataDictionary instance.</param>
    /// <param name="key">The key associated with the desired TempData value.</param>
    /// <returns>The deserialized object of type T, or null if not found.</returns>
    public static T? Peek<T>(this ITempDataDictionary tempData, string key) where T : class
    {
        var o = tempData.Peek(key);
        return o == null ? null : JsonSerializer.Deserialize<T>((string)o);
    }

    /// <summary>
    /// Serializes and sets an object in TempData with the specified key.
    /// </summary>
    /// <typeparam name="T">The type of the object to set.</typeparam>
    /// <param name="tempData">The ITempDataDictionary instance.</param>
    /// <param name="key">The key with which the object will be associated.</param>
    /// <param name="value">The object to set in TempData.</param>
    public static void Set<T>(this ITempDataDictionary tempData, string key, T value) where T : class
    {
        tempData[key] = JsonSerializer.Serialize(value);
    }
}