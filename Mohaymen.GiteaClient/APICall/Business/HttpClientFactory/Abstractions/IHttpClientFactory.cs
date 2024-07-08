using System;
using System.Net.Http;

namespace Mohaymen.GiteaClient.APICall.Business.HttpClientFactory.Abstractions;

internal interface IHttpClientFactory
{
    HttpClient CreateHttpClient(TimeSpan connectionTimeout);
}