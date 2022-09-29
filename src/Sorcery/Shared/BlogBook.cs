// Licensed under MIT, copyright Mateusz Gienieczko, all rights reserved.

using Sorcery.Blogging;

namespace Sorcery.Shared;

public sealed class BlogBook
{
    private readonly List<Post> _posts;

    public IEnumerable<Post> Posts => 
        _posts.Where(p => p.DateOfPublication is not null).OrderByDescending(p => p.DateOfPublication!.Value);

    public BlogBook()
    {
        _posts = new()
        {
            new ("SIMD &ndash; Sweet Intrinsics to Make (your) Day", "simd-sweet-instrinsics-to-make-your-day", null)
        };
    }

    public Post SimdSweetIntrinsicsToMakeYourDay => _posts[0];
}
