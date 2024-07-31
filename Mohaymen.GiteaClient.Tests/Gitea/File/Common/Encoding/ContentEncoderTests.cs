using FluentAssertions;
using Mohaymen.GiteaClient.Gitea.File.Common.Encoding;
using Mohaymen.GiteaClient.Gitea.File.Common.Encoding.Abstraction;
using Xunit;

namespace Mohaymen.GiteaClient.Tests.Gitea.File.Common.Encoding;

public class ContentEncoderTests
{
    private readonly IContentEncoder _encoder;

    public ContentEncoderTests()
    {
        _encoder = new ContentEncoder();
    }

    [Fact]
    public void Base64Encode_ShouldReturnEncodedContent_WhenEver()
    {
        // Arrange
        var content = "Hello, World!";
        var expected = "SGVsbG8sIFdvcmxkIQ==";

        // Act
        var encodedContent = _encoder.Base64Encode(content);

        // Assert
        encodedContent.Should().Be(expected);
    }
}