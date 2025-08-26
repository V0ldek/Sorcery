// Licensed under MIT, copyright Mateusz Gienieczko, all rights reserved.

using Sorcery.Blogging;

namespace Sorcery.ServerSide;

public sealed class BlogBook
{
    private readonly List<Post> _standalonePosts;
    private readonly List<Series> _series;

    public IEnumerable<Post> Posts =>
        _standalonePosts.Where(p => p.IsPublished)
            .Concat(Series.SelectMany(s => s.Posts).Where(p => p.IsPublished))
            .OrderByDescending(p => p.DateOfPublication!.Value);

    public IEnumerable<Series> Series =>
        from series in _series
        let firstPublished = series.Posts.FirstOrDefault(p => p.IsPublished)
        where firstPublished is not null
        orderby firstPublished.DateOfPublication!.Value descending
        select series;

    public IEnumerable<Tag> AllTags =>
        _standalonePosts.SelectMany(p => p.Tags).Distinct();

    public BlogBook()
    {
        _standalonePosts =
        [
            new("Who Scatters Memory, Gathers Latency", "who-scatters-memory-gathers-latency", null)
            {
                Tags = new Tag[] { new("simd"), new("perf"), new("rust"), },
                Description = Pages.Sourcery.Posts.WhoScattersMemoryGathersLatency.Introduction,
                ShortDescription = "Poking the vectorised gather/scatter black box.",
            },
            new("SIMD &ndash; Cheat Codes for Free Performance", "simd-cheat-codes-for-free-performance",
                new DateTime(2023, 10, 22, 22, 00, 00, DateTimeKind.Utc))
            {
                Tags = new Tag[] { new("simd"), new("csharp"), new("perf"), },
                Description = Pages.Sourcery.Posts.SimdCheatCodesForFreePerformance.Introduction,
                ShortDescription = "Discovering the wonderful parallel universe of local parallelism.",
            },
            new("Alphabet Does Not Go A to Z", "alphabet-does-not-go-a-to-z",
                new DateTime(2023, 07, 10, 14, 00, 00, DateTimeKind.Utc))
            {
                Tags = new Tag[] { new("unicode"), new("i18n"), new("regex"), },
                Description = Pages.Sourcery.Posts.AlphabetDoesNotGoAToZ.Introduction,
                ShortDescription = "Checking if a character is a letter is harder than you think.",
            },
            new("No, I Am Not Afraid A Wizard Will Take My Job", "no-i-am-not-afraid-a-wizard-will-take-my-job",
                null)
            {
                Tags = new Tag[] { new("meta"), new("industry"), new("programming"), },
                Description = Pages.Sourcery.Posts.NoIAmNotAfraidAWizardWillTakeMyJob.Introduction,
                ShortDescription = "Magic still does not exist.",
            },
        ];
        _series =
        [
            new Series([
                new("Making Languages In Rust (MLIR) – Chapter 0", "making-languages-in-rust-chapter-0",
                    new DateTime(2025, 08, 04, 17, 00, 00, DateTimeKind.Utc))
                {
                    Tags = new Tag[] { new("rust"), new("mlir"), new("compilers"), new("toy tutorial") },
                    Description =
                        Pages.Sourcery.Posts.MakingLanguagesInRust.MakingLanguagesInRustChapterZero.Introduction,
                    ShortDescription = "Implementing the MLIR Toy Tutorial in Rust",
                },
                new("Making Languages In Rust (MLIR) – Chapter 1", "making-languages-in-rust-chapter-1",
                    new DateTime(2025, 08, 04, 17, 00, 01, DateTimeKind.Utc))
                {
                    Tags = new Tag[] { new("rust"), new("mlir"), new("compilers"), new("toy tutorial") },
                    Description =
                        Pages.Sourcery.Posts.MakingLanguagesInRust.MakingLanguagesInRustChapterOne.Introduction,
                    ShortDescription = "Implementing the MLIR Toy Tutorial in Rust – Toy parser",
                },
                new("Making Languages In Rust (MLIR) – Chapter 2", "making-languages-in-rust-chapter-2",
                    new DateTime(2025, 08, 04, 17, 00, 02, DateTimeKind.Utc))
                {
                    Tags = new Tag[] { new("rust"), new("mlir"), new("compilers"), new("toy tutorial") },
                    Description =
                        Pages.Sourcery.Posts.MakingLanguagesInRust.MakingLanguagesInRustChapterTwo.Introduction,
                    ShortDescription = "Implementing the MLIR Toy Tutorial in Rust – Toy codegen",
                },
                new("Making Languages In Rust (MLIR) – Chapter 3", "making-languages-in-rust-chapter-3",
                    null)
                {
                    Tags = new Tag[] { new("rust"), new("mlir"), new("compilers"), new("cpp"), new("toy tutorial") },
                    Description =
                        Pages.Sourcery.Posts.MakingLanguagesInRust.MakingLanguagesInRustChapterTwo.Introduction,
                    ShortDescription = "Implementing the MLIR Toy Tutorial in Rust – Talking with C++",
                },
                new("Making Languages In Rust (MLIR) – Chapter 4", "making-languages-in-rust-chapter-4",
                    null)
                {
                    Tags = new Tag[] { new("rust"), new("mlir"), new("compilers"), new("toy tutorial") },
                    Description =
                        Pages.Sourcery.Posts.MakingLanguagesInRust.MakingLanguagesInRustChapterTwo.Introduction,
                    ShortDescription = "Implementing the MLIR Toy Tutorial in Rust – Snapshot testing",
                },
                new("Making Languages In Rust (MLIR) – Chapter 5", "making-languages-in-rust-chapter-5",
                    null)
                {
                    Tags = new Tag[] { new("rust"), new("mlir"), new("compilers"), new("toy tutorial") },
                    Description =
                        Pages.Sourcery.Posts.MakingLanguagesInRust.MakingLanguagesInRustChapterTwo.Introduction,
                    ShortDescription = "Implementing the MLIR Toy Tutorial in Rust – MLIR canonicalization patterns",
                },
                new("Making Languages In Rust (MLIR) – Chapter 6", "making-languages-in-rust-chapter-6",
                    null)
                {
                    Tags = new Tag[] { new("rust"), new("mlir"), new("compilers"), new("toy tutorial") },
                    Description =
                        Pages.Sourcery.Posts.MakingLanguagesInRust.MakingLanguagesInRustChapterTwo.Introduction,
                    ShortDescription = "Implementing the MLIR Toy Tutorial in Rust – MLIR interfaces",
                },
                new("Making Languages In Rust (MLIR) – Chapter 7", "making-languages-in-rust-chapter-7",
                    null)
                {
                    Tags = new Tag[] { new("rust"), new("mlir"), new("compilers"), new("toy tutorial") },
                    Description =
                        Pages.Sourcery.Posts.MakingLanguagesInRust.MakingLanguagesInRustChapterTwo.Introduction,
                    ShortDescription = "Implementing the MLIR Toy Tutorial in Rust – Lowering",
                },
                new("Making Languages In Rust (MLIR) – Chapter 8", "making-languages-in-rust-chapter-8",
                    null)
                {
                    Tags = new Tag[] { new("rust"), new("mlir"), new("compilers"), new("toy tutorial") },
                    Description =
                        Pages.Sourcery.Posts.MakingLanguagesInRust.MakingLanguagesInRustChapterTwo.Introduction,
                    ShortDescription = "Implementing the MLIR Toy Tutorial in Rust – LLVM, JIT",
                },
                new("Making Languages In Rust (MLIR) – Chapter 9", "making-languages-in-rust-chapter-9",
                    null)
                {
                    Tags = new Tag[] { new("rust"), new("mlir"), new("compilers"), new("toy tutorial") },
                    Description =
                        Pages.Sourcery.Posts.MakingLanguagesInRust.MakingLanguagesInRustChapterTwo.Introduction,
                    ShortDescription = "Implementing the MLIR Toy Tutorial in Rust – Rust TableGen",
                },
                new("Making Languages In Rust (MLIR) – Chapter 10", "making-languages-in-rust-chapter-10",
                    null)
                {
                    Tags = new Tag[] { new("rust"), new("mlir"), new("compilers"), new("toy tutorial") },
                    Description =
                        Pages.Sourcery.Posts.MakingLanguagesInRust.MakingLanguagesInRustChapterTwo.Introduction,
                    ShortDescription = "Implementing the MLIR Toy Tutorial in Rust – Composite type",
                }
            ])
        ];
    }

    public Post WhoScattersMemoryGathersLatency => _standalonePosts[0];

    public Post SimdCheatCodesForFreePerformance => _standalonePosts[1];

    public Post AlphabetDoesNotGoAToZ => _standalonePosts[2];

    public Post NoIAmNotAfraidAWizardWillTakeMyJob => _standalonePosts[3];

    public Series MakingLanguagesInRust => _series[0];
}
