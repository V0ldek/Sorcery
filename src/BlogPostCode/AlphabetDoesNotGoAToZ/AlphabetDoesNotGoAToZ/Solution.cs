// Licensed under MIT, copyright Mateusz Gienieczko, all rights reserved.

using System.Text.RegularExpressions;

namespace AlphabetDoesNotGoAToZ;

public interface ISolution
{
    bool IsAllLetters(string text);
}

public sealed class IncorrectSolution : ISolution
{
    public bool IsAllLetters(string text) =>
        text.All(c => (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'));
}

public sealed class IncorrectRegexSolution : ISolution
{
    // If you're not regex-savvy, this matches a full string that consists
    // of an arbitrary number of characters either in a-z or A-Z.
    private static readonly Regex AllLettersRegex = new("^[a-zA-Z]*$");

    public bool IsAllLetters(string text) => AllLettersRegex.IsMatch(text);
}

public sealed class CorrectSolution : ISolution
{
    public bool IsAllLetters(string text) => text.All(c => char.IsLetter(c));
}

public sealed class CorrectRegexSolution : ISolution
{
    private static readonly Regex AllLettersRegex = new(@"^\p{L}*$");

    public bool IsAllLetters(string text) => AllLettersRegex.IsMatch(text);
}

