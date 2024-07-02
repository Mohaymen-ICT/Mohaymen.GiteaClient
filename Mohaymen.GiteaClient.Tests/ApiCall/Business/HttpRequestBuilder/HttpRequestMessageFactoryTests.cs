using System.Collections.Specialized;
using FluentAssertions;
using Mohaymen.GitClient.APICall.Business.HttpRequestBuilder;
using Mohaymen.GitClient.APICall.Business.HttpRequestBuilder.Abstractions;
using Xunit;

namespace Mohaymen.GitClient.Tests.ApiCall.Business.HttpRequestBuilder;

public class HttpRequestMessageFactoryTests
{
    private readonly IHttpRequestMessageFactory _sut;

    public HttpRequestMessageFactoryTests()
    {
        _sut = new HttpRequestMessageFactory();
    }

    [Fact]
    public void CreateHttpRequestMessage_ShouldReturnHttpRequestMessage_WhenInputsAreProvided()
    {
        // Arrange
        var requestHeaders = new NameValueCollection()
        {
            { "Authorization", "token 1234567890" }
        };
        var contentHeaders = new NameValueCollection()
        {
            { "Content-Type", "application/json" }
        };
        const string url = "http://www.google.com";
        const string requestBody = "fakeRequestBody";
        var httpMethod = HttpMethod.Delete;
        var expected = new HttpRequestMessage()
        {
            Content = new StringContent(requestBody),
            RequestUri = new Uri(url),
            Method = HttpMethod.Delete,
            Headers =
            {
                { "Authorization", "token 1234567890" },
            }
        };
        expected.Content.Headers.Remove("content-type");
        expected.Content.Headers.Add("Content-Type", "application/json");

        // Act
        var actual = _sut.CreateHttpRequestMessage(url, httpMethod, requestBody, requestHeaders, contentHeaders);

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }
}