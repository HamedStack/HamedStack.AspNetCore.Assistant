// ReSharper disable UnusedMember.Global
// ReSharper disable CheckNamespace
// ReSharper disable UnusedType.Global

using Microsoft.AspNetCore.Http;

namespace HamedStack.AspNetCore.Assistant.Extensions.FormFileExtended;

/// <summary>
/// Provides extension methods for the <see cref="IFormFile"/> interface to simplify
/// data extraction and manipulation.
/// </summary>
public static class FormFileExtensions
{
    /// <summary>
    /// Converts the contents of an <see cref="IFormFile"/> into a byte array.
    /// </summary>
    /// <param name="file">The instance of <see cref="IFormFile"/> from which to extract data.</param>
    /// <returns>A byte array containing the file's data.</returns>
    public static byte[] ToByteArray(this IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        file.CopyTo(memoryStream);
        return memoryStream.ToArray();
    }

    /// <summary>
    /// Converts the contents of an <see cref="IFormFile"/> into a byte array asynchronously.
    /// </summary>
    /// <param name="file">The instance of <see cref="IFormFile"/> from which to extract data.</param>
    /// <returns>A task that represents the asynchronous operation. 
    /// The task result contains a byte array with the file's data.</returns>
    public static async Task<byte[]> ToByteArrayAsync(this IFormFile file)
    {
        await using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream).ConfigureAwait(false);
        return memoryStream.ToArray();
    }

    /// <summary>
    /// Converts the contents of an <see cref="IFormFile"/> into a <see cref="Stream"/>.
    /// </summary>
    /// <param name="file">The instance of <see cref="IFormFile"/> from which to extract data.</param>
    /// <returns>A <see cref="Stream"/> containing the file's data.</returns>
    public static Stream ToStream(this IFormFile file)
    {
        var stream = new MemoryStream();
        file.CopyTo(stream);
        stream.Position = 0;

        return stream;
    }

    /// <summary>
    /// Converts the contents of an <see cref="IFormFile"/> into a <see cref="Stream"/> asynchronously.
    /// </summary>
    /// <param name="file">The instance of <see cref="IFormFile"/> from which to extract data.</param>
    /// <returns>A task that represents the asynchronous operation. 
    /// The task result contains a <see cref="Stream"/> with the file's data.</returns>
    public static async Task<Stream> ToStreamAsync(this IFormFile file)
    {
        var stream = new MemoryStream();
        await file.CopyToAsync(stream);
        stream.Position = 0;
        return stream;
    }
}