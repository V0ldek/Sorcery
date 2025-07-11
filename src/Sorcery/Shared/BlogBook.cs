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
            SimdPartTwoIntrinsicsAndYou,
            SimdCheatCodesForFreePerformance,
            AlphabetDoesNotGoAToZ
        };
    }

    public Post SimdPartTwoIntrinsicsAndYou => 
        new ("SIMD Part II &ndash; Intrinsics and You", "simd-part-two-intrinsics-and-you", null)
        {
            Tags = new Tag[]
            {
                new ("simd"),
                new ("csharp"),
                new ("perf"),
            },
            Description = Pages.Sourcery.Posts.SimdPartTwoIntrinsicsAndYou.Introduction,
            ShortDescription = "Discovering the intrinsically fascinating world of compiler intrinsics.",
        };
    
    public Post SimdCheatCodesForFreePerformance => 
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
        };

    public Post AlphabetDoesNotGoAToZ => 
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
        };
}
