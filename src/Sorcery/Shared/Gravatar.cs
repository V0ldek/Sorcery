// Licensed under MIT, copyright Mateusz Gienieczko, all rights reserved.

namespace Sorcery.Shared;

/// <summary>
///     Utilities for creating Gravatar links.
/// </summary>
public static class Gravatar
{
    /// <summary>
    ///     MD5 hash of V0ldek's email to link to his gravatar.
    /// </summary>
    // The hash could be computed on-the-fly from the email, but there is no need
    // to introduce System.Security.Cryptography into the WebAssembly pack.
    public static string V0ldek => "d86a9e012bc4281b2007b082029d7dfd";

    /// <summary>
    ///     Create a Gravatar link for the given <paramref name="hash"/>.
    /// </summary>
    /// <param name="hash">Email MD5 hash to use for the Gravatar.</param>
    /// <returns>Url referencing the Gravatar image.</returns>
    /// <remarks>
    ///     Typical usage would be with the <see cref="MudBlazor.MudAvatar"/> component:
    ///     
    ///     <code>
    ///         <MudAvatar Image=@Gravatar.For(Gravatar.V0ldek).ToString() Alt="my gravatar"></MudAvatar>
    ///     </code>
    ///     
    ///     Note that the <paramref name="hash"/> is not validated, so the url might not be valid.
    ///     The hashes defined as statics in <see cref="Gravatar"/> are guaranteed to be valid.
    /// </remarks>
    public static Uri For(string hash) => new($"https://gravatar.com/avatar/{hash}");

    /// <summary>
    ///     Create a Gravatar link for the given <paramref name="hash"/> requesting a given size of
    ///     the Gravatar.
    /// </summary>
    /// <param name="hash">Email MD5 hash to use for the Gravatar.</param>
    /// <param name="size">Requested size in pixels, between 1 and 2048.</param>
    /// <returns>Url referencing the Gravatar image.</returns>
    /// <remarks>
    ///     Typical usage would be with the <see cref="MudBlazor.MudAvatar"/> component:
    ///     
    ///     <code>
    ///         <MudAvatar Image=@Gravatar.For(Gravatar.V0ldek, 180).ToString() Alt="my gravatar"></MudAvatar>
    ///     </code>
    ///     
    ///     Note that the <paramref name="hash"/> is not validated, so the url might not be valid.
    ///     The hashes defined as statics in <see cref="Gravatar"/> are guaranteed to be valid.
    /// </remarks>
    public static Uri For(string hash, int size)
    {
        if (size < 1 || size > 2048)
        {
            throw new ArgumentOutOfRangeException(nameof(size), "Must be between 1 and 2048 pixels.");
        }

        Uri uri = new($"https://gravatar.com/avatar/{hash}?s={size}");
        return uri;
    }
}
