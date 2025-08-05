// Licensed under MIT, copyright Mateusz Gienieczko, all rights reserved.

namespace Sorcery.Blogging;

public sealed class Series
{
    public IReadOnlyList<Post> Posts { get; }

    public Series(IEnumerable<Post> posts)
    {
        Posts = posts.ToArray();
        foreach (var (post, idx) in Posts.Select((x, i) => (x, i)))
        {
            post.AddToSeries(this, idx);
        }
    }
}
