// Licensed under MIT, copyright Mateusz Gienieczko, all rights reserved.

namespace AlphabetDoesNotGoAToZ.UnitTests
{
    public class SolutionUnitTests
    {
        private static readonly IReadOnlyList<string> BasicAlphaStrings = new[]
        {
            "helloworld", "HELLOWORLD", "HelloWorld", ""
        };

        private static readonly IReadOnlyList<string> BasicNonAlphaStrings = new[]
        {
            "Hello, World!", "h3110w0r1d", "hello_world", "hello::world"
        };

        private static readonly IReadOnlyList<string> NonLatinAlphaStrings = new[]
        {
            "Witajświecie", "Привiтсвiте", "سلامدنیا", "你好世界", "naïveSolutions"
        };

        private static IReadOnlyList<ISolution> CreateSolutions() => new ISolution[]
        {
            new IncorrectSolution(),
            new IncorrectRegexSolution(),
            new CorrectSolution(),
            new CorrectRegexSolution(),
        };

        private static TheoryData<ISolution, string> CreateTestData(IEnumerable<string> strings)
        {
            var data = new TheoryData<ISolution, string>();

            foreach (var implementation in CreateSolutions())
            {
                foreach (var text in strings)
                {
                    data.Add(implementation, text);
                }
            }

            return data;
        }

        public static TheoryData<ISolution, string> BasicAlphaTestData => CreateTestData(BasicAlphaStrings);

        public static TheoryData<ISolution, string> BasicNonAlphaTestData => CreateTestData(BasicNonAlphaStrings);

        public static TheoryData<ISolution, string> NonLatinAlphaTestData => CreateTestData(NonLatinAlphaStrings);

        [Theory]
        [MemberData(nameof(BasicAlphaTestData))]
        [MemberData(nameof(NonLatinAlphaTestData))]
        public void IsAllLetters_WithAlphaStrings_ShouldBeTrue(ISolution solution, string text)
        {
            Assert.True(solution.IsAllLetters(text));
        }

        [Theory]
        [MemberData(nameof(BasicNonAlphaTestData))]
        public void IsAllLetters_WithNonAlphaStrings_ShouldBeFalse(ISolution solution, string text)
        {
            Assert.False(solution.IsAllLetters(text));
        }
    }
}
