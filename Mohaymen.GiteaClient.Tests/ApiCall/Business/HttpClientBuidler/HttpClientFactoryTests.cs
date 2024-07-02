using FluentAssertions;
using Mohaymen.GitClient.APICall.Business.HttpClientFactory;
using Mohaymen.GitClient.APICall.Business.HttpClientFactory.Abstractions;
using Xunit;

namespace Mohaymen.GitClient.Tests.ApiCall.Business.HttpClientBuidler;

public class HttpClientFactoryTests
{
    private readonly IHttpClientFactory _sut;

    public HttpClientFactoryTests()
    {
        _sut = new HttpClientFactory();
    }

    [Fact]
    public void GetHttpClient_ShouldCreateHttpClientWithCorrectTimeout_WhenEver()
    {
        // Arrange
        var connectionTimeout = TimeSpan.FromHours(1);
        
        // Act
        var actual = _sut.CreateHttpClient(connectionTimeout);

        // Assert
        actual.Timeout.Should().Be(connectionTimeout);
    }
    
}