// Licensed under MIT, copyright Mateusz Gienieczko, all rights reserved.

namespace Sorcery.Blogging;

public readonly record struct Tag
{
    public static readonly Tag None;

    public string Value { get; private init; }

    public Tag(string tag) => Value = NormalizeTag(tag);

    private static string NormalizeTag(string tag)
    {
        var words = tag.Split().Select(w => w.ToLowerInvariant());

        return string.Join('-', words);
    }

    public override string ToString() => Value ?? "None";
}
