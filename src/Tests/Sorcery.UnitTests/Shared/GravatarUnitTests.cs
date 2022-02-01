// Licensed under MIT, copyright Mateusz Gienieczko, all rights reserved.

using System.Security.Cryptography;
using System.Text;
using Sorcery.Shared;

namespace Sorcery.UnitTests.Shared;

public class GravatarUnitTests
{
    [Fact]
    public void V0ldekGravatar_IsAnMD5HashOfV0ldeksEmail()
    {
        // Obfuscate the string so it cannot be simply scraped from the repo.
        var email = string.Format("{0}@{1}.com", "mat", "gienieczko");

        var bytes = Encoding.ASCII.GetBytes(email);
        using var md5 = MD5.Create();
        var hashBytes = md5.ComputeHash(bytes);
        var hexString = BitConverter.ToString(hashBytes).Replace("-", "");

        var actual = Gravatar.V0ldek;

        actual.Should().BeEquivalentTo(hexString);
    }

    [Fact]
    public void For_GivenMd5Hash_ReturnsCorrectGravatarUrl()
    {
        const string TestHash = "abcdef123456789";
        var expected = new Uri("https://gravatar.com/avatar/abcdef123456789");

        var actual = Gravatar.For(TestHash);

        actual.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(180)]
    [InlineData(256)]
    [InlineData(2048)]
    public void For_GivenMd5HashAndSize_ReturnsCorrectGravatarUrlWithSizeQueryParameter(int size)
    {
        const string TestHash = "abcdef123456789";
        var expected = new Uri($"https://gravatar.com/avatar/abcdef123456789?s={size}");

        var actual = Gravatar.For(TestHash, size);

        actual.Should().BeEquivalentTo(expected);
    }


    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(2049)]
    public void For_GivenMd5HashAndInvalidSize_ThrowsArgumentOutOfRangeException(int size)
    {
        const string TestHash = "abcdef123456789";

        Assert.Throws<ArgumentOutOfRangeException>(() => Gravatar.For(TestHash, size));
    }
}
