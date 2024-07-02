using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Mohaymen.GitClient.APICall.Business.HttpClientFactory.Abstractions;
using Mohaymen.GitClient.APICall.Business.HttpRequestBuilder.Abstractions;
using Mohaymen.GitClient.APICall.Business.Serialization.Abstractions;
using Mohaymen.GitClient.APICall.Business.Wrappers.Abstractions;
using Mohaymen.GitClient.APICall.Domain;
using Mohaymen.GitClient.APICall.Facades.Abstractions;

namespace Mohaymen.GitClient.APICall.Facades;

internal class ApiCallFacade : IApiCallFacade
{
    private readonly IHttpRequestMessageFactory _httpRequestMessageFactory;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IJsonSerializer _jsonSerializer;
    private readonly IHttpClientWrapper _httpClientWrapper;
    private readonly GiteaApiConfiguration _giteaApiConfiguration;

    public ApiCallFacade(IHttpRequestMessageFactory httpRequestMessageFactory,
        IHttpClientFactory httpClientFactory, 
        GiteaApiConfiguration giteaApiConfiguration,
        IJsonSerializer jsonSerializer,
        IHttpClientWrapper httpClientWrapper)
    {
        _httpRequestMessageFactory = httpRequestMessageFactory ?? throw new ArgumentNullException(nameof(httpRequestMessageFactory));
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _giteaApiConfiguration = giteaApiConfiguration ?? throw new ArgumentNullException(nameof(giteaApiConfiguration));
        _jsonSerializer = jsonSerializer ?? throw new ArgumentNullException(nameof(jsonSerializer));
        _httpClientWrapper = httpClientWrapper ?? throw new ArgumentNullException(nameof(httpClientWrapper));
    }
    
    public async Task<HttpResponseDto<TResponseDto>> SendAsync<TRequestDto, TResponseDto>(HttpRestApiDto<TRequestDto> httpRestApiDto, CancellationToken cancellationToken = default)
    {
        var jsonBody = _jsonSerializer.SerializeObject(httpRestApiDto.BodyDto!);
        using var httpClient = _httpClientFactory.CreateHttpClient(_giteaApiConfiguration.ApiConnectionTimeout);
        var httpRequestHeaders = CreateHttpRequestHeaders(_giteaApiConfiguration.PersonalAccessToken);
        var httpContentHeaders = CreateContentHeaders();
        using var httpRequestMessage = _httpRequestMessageFactory.CreateHttpRequestMessage(_giteaApiConfiguration.BaseUrl,
            httpRestApiDto.HttpMethod,
            jsonBody,
            httpRequestHeaders,
            httpContentHeaders);
        var httpResponseMessage = await _httpClientWrapper.SendAsync(httpClient, httpRequestMessage, cancellationToken).ConfigureAwait(false);
        var statusCode = (int) httpResponseMessage.StatusCode;
        var responseString = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
        if (statusCode is < 200 or >= 300)
            return new HttpResponseDto<TResponseDto>()
            {
                IsSuccessfull = false,
                ErrorMessage = responseString
            };
        var responseDto = _jsonSerializer.DeserializeJson<TResponseDto>(responseString);
        return new HttpResponseDto<TResponseDto>()
        {
            IsSuccessfull = true,
            ResponseBody = responseDto
        };
    }

    private static NameValueCollection CreateHttpRequestHeaders(string token)
    {
        return new NameValueCollection { { "Authorization", $"token {token}" } };
    }

    private static NameValueCollection CreateContentHeaders()
    {
        return new NameValueCollection { { "Content-Type", "application/json" } };
    }
}