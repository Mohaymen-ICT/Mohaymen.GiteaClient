using System;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using Mohaymen.GiteaClient.APICall.Business.HttpClientFactory.Abstractions;
using Mohaymen.GiteaClient.APICall.Business.HttpRequestBuilder.Abstractions;
using Mohaymen.GiteaClient.APICall.Business.Serialization.Abstractions;
using Mohaymen.GiteaClient.APICall.Business.Wrappers.Abstractions;
using Mohaymen.GiteaClient.APICall.Domain;
using Mohaymen.GiteaClient.APICall.Facades.Abstractions;

namespace Mohaymen.GiteaClient.APICall.Facades;

internal sealed class ApiCallFacade : IApiCallFacade
{
    private readonly IHttpRequestMessageFactory _httpRequestMessageFactory;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IJsonSerializer _jsonSerializer;
    private readonly IHttpClientWrapper _httpClientWrapper;
    private readonly IOptions<GiteaApiConfiguration> _giteaOptions;

    public ApiCallFacade(IHttpRequestMessageFactory httpRequestMessageFactory,
        IHttpClientFactory httpClientFactory, 
        IOptions<GiteaApiConfiguration> giteaOptions,
        IJsonSerializer jsonSerializer,
        IHttpClientWrapper httpClientWrapper)
    {
        _httpRequestMessageFactory = httpRequestMessageFactory ?? throw new ArgumentNullException(nameof(httpRequestMessageFactory));
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _giteaOptions = giteaOptions ?? throw new ArgumentNullException(nameof(giteaOptions));
        _jsonSerializer = jsonSerializer ?? throw new ArgumentNullException(nameof(jsonSerializer));
        _httpClientWrapper = httpClientWrapper ?? throw new ArgumentNullException(nameof(httpClientWrapper));
    }
    
    public async Task<GiteaResponseDto<TResponseDto>> SendAsync<TRequestDto, TResponseDto>(HttpRestApiDto<TRequestDto> httpRestApiDto, CancellationToken cancellationToken = default)
        where TRequestDto: IRequest<GiteaResponseDto<TResponseDto>>
    {
        var jsonBody = _jsonSerializer.SerializeObject(httpRestApiDto.BodyDto!);
        using var httpClient = _httpClientFactory.CreateHttpClient(_giteaOptions.Value.ApiConnectionTimeout);
        var httpRequestHeaders = CreateHttpRequestHeaders(_giteaOptions.Value.PersonalAccessToken);
        var httpContentHeaders = CreateContentHeaders();
        using var httpRequestMessage = _httpRequestMessageFactory.CreateHttpRequestMessage(_giteaOptions.Value.BaseUrl,
            httpRestApiDto.HttpMethod,
            jsonBody,
            httpRequestHeaders,
            httpContentHeaders);
        var httpResponseMessage = await _httpClientWrapper.SendAsync(httpClient, httpRequestMessage, cancellationToken).ConfigureAwait(false);
        var statusCode = (int) httpResponseMessage.StatusCode;
        var responseString = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
        if (statusCode is < 200 or >= 300)
            return new GiteaResponseDto<TResponseDto>()
            {
                IsSuccessfull = false,
                ErrorMessage = responseString
            };
        var responseDto = _jsonSerializer.DeserializeJson<TResponseDto>(responseString);
        return new GiteaResponseDto<TResponseDto>()
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