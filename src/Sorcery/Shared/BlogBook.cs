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
            new ("Who Scatters Memory, Gathers Latency", "who-scatters-memory-gathers-latency", null)
            {
                Tags = new Tag[]
                {
                    new ("simd"),
                    new ("perf"),
                    new ("rust"),
                },
                Description = Pages.Sourcery.Posts.WhoScattersMemoryGathersLatency.Introduction,
                ShortDescription = "Poking the vectorised gather/scatter black box.",
            },
            new ("SIMD &ndash; Cheat Codes for Free Performance", "simd-cheat-codes-for-free-performance", new DateTime(2023, 10, 22, 22, 00, 00, DateTimeKind.Utc))
            {
                Tags = new Tag[]
                {
                    new ("simd"),
                    new ("csharp"),
                    new ("perf"),
                },
                Description = Pages.Sourcery.Posts.SimdCheatCodesForFreePerformance.Introduction,
                ShortDescription = "Discovering the wonderful parallel universe of local parallelism.",
            },
            new ("Alphabet Does Not Go A to Z", "alphabet-does-not-go-a-to-z", new DateTime(2023, 07, 10, 14, 00, 00, DateTimeKind.Utc))
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

    public Post WhoScattersMemoryGathersLatency => _posts[0];

    public Post SimdCheatCodesForFreePerformance => _posts[1];

    public Post AlphabetDoesNotGoAToZ => _posts[2];
}
