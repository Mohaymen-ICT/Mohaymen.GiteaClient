using FluentAssertions;
using Mohaymen.GiteaClient.Gitea.File.Common.Encoding;
using Mohaymen.GiteaClient.Gitea.File.Common.Encoding.Abstraction;
using Xunit;

namespace Mohaymen.GiteaClient.Tests.Gitea.File.Common.Encoding;

public class ContentDecoderTests
{
    private readonly IContentDecoder _decoder;

    public ContentDecoderTests()
    {
        _decoder = new ContentDecoder();
    }

    [Fact]
    public void Base64Decode_ShouldReturnDecodedContent_WhenEver()
    {
        // Arrange
        var encodedContent = "SGVsbG8sIFdvcmxkIQ==";
        var expectedDecodedContent = "Hello, World!";

        // Act
        var decodedContent = _decoder.Base64Decode(encodedContent);

        // Assert
        decodedContent.Should().Be(expectedDecodedContent);
    }
}