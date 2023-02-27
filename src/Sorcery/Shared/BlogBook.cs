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
            new ("SIMD &ndash; Sweet Intrinsics to Make (your) Day", "simd-sweet-intrinsics-to-make-your-day", null)
            {
                Tags = new Tag[]
                {
                    new ("simd"),
                    new ("csharp"),
                },
                Description = Pages.Sourcery.Posts.SimdSweetIntrinsicsToMakeYourDay.Introduction,
                ShortDescription = "Discovering the wonderful parallel universe of local parallelism.",
            },
            new ("Alphabet does not go A to Z", "alphabet-does-not-go-a-to-z", new DateTime(2022, 11, 30))
            {
                Tags = new Tag[]
                {
                    new ("unicode"),
                    new ("i18n"),
                    new ("regex"),
                },
                Description = Pages.Sourcery.Posts.AlphabetDoesNotGoAToZ.Introduction,
                ShortDescription = "Checking if a character is a letter is harder than you think.",
            },
        };
    }

    public Post SimdSweetIntrinsicsToMakeYourDay => _posts[0];

    public Post AlphabetDoesNotGoAToZ => _posts[1];
}
