﻿using System.Collections.Specialized;
using System.Net;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Mohaymen.GitClient.Tests.Mocks;
using Mohaymen.GiteaClient.APICall.Business.HttpClientFactory.Abstractions;
using Mohaymen.GiteaClient.APICall.Business.HttpRequestBuilder.Abstractions;
using Mohaymen.GiteaClient.APICall.Business.Serialization.Abstractions;
using Mohaymen.GiteaClient.APICall.Business.Wrappers.Abstractions;
using Mohaymen.GiteaClient.APICall.Domain;
using Mohaymen.GiteaClient.APICall.Facades;
using Mohaymen.GiteaClient.APICall.Facades.Abstractions;
using NSubstitute;
using Xunit;

namespace Mohaymen.GitClient.Tests.ApiCall.Facades;

public class ApiCallFacadeTests
{
    private readonly IHttpRequestMessageFactory _httpRequestMessageFactory;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IJsonSerializer _jsonSerializer;
    private readonly IOptions<GiteaApiConfiguration> _giteaApiConfigurationOptions;
    private readonly IHttpClientWrapper _httpClientWrapper;
    private readonly IApiCallFacade _sut;
    private const string BaseUrl = "http://www.google.com";
    private const string Token = "12345678";

    public ApiCallFacadeTests()
    {
        _httpRequestMessageFactory = Substitute.For<IHttpRequestMessageFactory>();
        _httpClientFactory = Substitute.For<IHttpClientFactory>();
        _jsonSerializer = Substitute.For<IJsonSerializer>();
        _giteaApiConfigurationOptions = Substitute.For<IOptions<GiteaApiConfiguration>>();
        _httpClientWrapper = Substitute.For<IHttpClientWrapper>();
        _sut = new ApiCallFacade(_httpRequestMessageFactory, _httpClientFactory, _giteaApiConfigurationOptions,
            _jsonSerializer, _httpClientWrapper);
    }

    [Fact]
    public async Task SendAsync_ShouldReturnResponse_WhenApiReturnsNotSuccessStatusCode()
    {
        // Arrange
        var requestBodyDto = new FakeRequestBody()
        {
            Age = 23,
            Name = "alireza"
        };
        var httpMethod = HttpMethod.Post;
        var httpRestApiDto = new HttpRestApiDto<FakeRequestBody>()
        {
            BodyDto = requestBodyDto,
            HttpMethod = httpMethod
        };
        const string jsonBody = "fakeJsonBody";
        _jsonSerializer.SerializeObject(requestBodyDto).Returns(jsonBody);
        using var httpClient = new HttpClient();
        _httpClientFactory.CreateHttpClient(TimeSpan.FromHours(1)).Returns(httpClient);
        using var httpRequestMessage = new HttpRequestMessage();
        _httpRequestMessageFactory.CreateHttpRequestMessage(BaseUrl,
            httpMethod,
            jsonBody,
            Arg.Is<NameValueCollection>(x => x.Count == 1 &&
                                             x.AllKeys.Contains("Authorization") &&
                                             x.GetValues("Authorization")[0] == "token 12345678"),
            Arg.Is<NameValueCollection>(x => x.Count == 1 &&
                                             x.AllKeys.Contains("Content-Type") &&
                                             x.GetValues("Content-Type")[0] == "application/json")
        ).Returns(httpRequestMessage);
        const string errorMessage = "error in request";
        _httpClientWrapper.SendAsync(httpClient, httpRequestMessage, default)
            .Returns(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.Forbidden,
                Content = new StringContent(errorMessage)
            });
        var giteaConfiguration = new GiteaApiConfiguration
        {
            BaseUrl = BaseUrl,
            PersonalAccessToken = Token,
            RepositoriesOwner = "fakeOwner"
        };
        _giteaApiConfigurationOptions.Value.Returns(giteaConfiguration);
        var expected = new GiteaResponseDto<FakeResponseBody>
        {
            IsSuccessfull = false,
            ErrorMessage = errorMessage
        };

        // Act
        var actual = await _sut.SendAsync<FakeRequestBody, FakeResponseBody>(httpRestApiDto, default);

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task SendAsync_ShouldReturnResponse_WhenApiReturnsSuccessStatusCode()
    {
        // Arrange
        var requestBodyDto = new FakeRequestBody()
        {
            Age = 23,
            Name = "alireza"
        };
        var httpMethod = HttpMethod.Post;
        var httpRestApiDto = new HttpRestApiDto<FakeRequestBody>()
        {
            BodyDto = requestBodyDto,
            HttpMethod = httpMethod
        };
        const string jsonBody = "fakeJsonBody";
        _jsonSerializer.SerializeObject(requestBodyDto).Returns(jsonBody);
        using var httpClient = new HttpClient();
        _httpClientFactory.CreateHttpClient(TimeSpan.FromHours(1)).Returns(httpClient);
        using var httpRequestMessage = new HttpRequestMessage();
        _httpRequestMessageFactory.CreateHttpRequestMessage(BaseUrl,
            httpMethod,
            jsonBody,
            Arg.Is<NameValueCollection>(x => x.Count == 1 &&
                                             x.AllKeys.Contains("Authorization") &&
                                             x.GetValues("Authorization")[0] == "token 12345678"),
            Arg.Is<NameValueCollection>(x => x.Count == 1 &&
                                             x.AllKeys.Contains("Content-Type") &&
                                             x.GetValues("Content-Type")[0] == "application/json")
        ).Returns(httpRequestMessage);
        const string responseString = "responseString";
        const string responseBodyString = "fakeBody";
        var responseBody = new FakeResponseBody
        {
            Body = responseBodyString
        };
        _jsonSerializer.DeserializeJson<FakeResponseBody>(responseString).Returns(responseBody);
        _httpClientWrapper.SendAsync(httpClient, httpRequestMessage, default)
            .Returns(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(responseString)
            });
        var giteaConfiguration = new GiteaApiConfiguration
        {
            BaseUrl = BaseUrl,
            PersonalAccessToken = Token,
            RepositoriesOwner = "fakeOwner"
        };
        _giteaApiConfigurationOptions.Value.Returns(giteaConfiguration);
        var expected = new GiteaResponseDto<FakeResponseBody>
        {
            IsSuccessfull = true,
            ResponseBody = new FakeResponseBody()
            {
                Body = responseBodyString
            }
        };

        // Act
        var actual = await _sut.SendAsync<FakeRequestBody, FakeResponseBody>(httpRestApiDto, default);

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }
}