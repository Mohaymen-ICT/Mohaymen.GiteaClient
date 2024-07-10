using FluentAssertions;
using Mohaymen.GiteaClient.Core.ApiCall.Exceptions;
using Mohaymen.GiteaClient.Core.ApiCall.HttpHeader;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace Mohaymen.GitClient.Tests.Core.HttpHeader;

public class HttpHeaderFactoryTests
{
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void GetAuthorizationHeader_ShouldThrowInvalidApiKeyException_WhenTokenIsNullOrEmpty(string token)
    {
        // Arrange
        
        // Act
        var actual = () => HttpHeaderFactory.GetAuthorizationHeader(token);
        
        // Assert
        actual.Should().Throws<InvalidApiKeyException>();
    }
    
    [Fact]
    public void GetAuthorizationHeader_ShouldReturnApiKey_WhenTokenIsValid()
    {
        // Arrange
        const string token = "123456";
        const string expected = "token 123456";

        // Act
        var actual = HttpHeaderFactory.GetAuthorizationHeader(token);

        // Assert
        actual.Should().Be(expected);
    }
}