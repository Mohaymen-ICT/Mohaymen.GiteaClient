using System;
using System.Net.Http;
using Mohaymen.GitClient.APICall.Business.HttpClientFactory.Abstractions;

namespace Mohaymen.GitClient.APICall.Business.HttpClientFactory;

internal class HttpClientFactory : IHttpClientFactory
{
    public HttpClient CreateHttpClient(TimeSpan connectionTimeout)
    {
        var httpClientHandler = new HttpClientHandler();
        httpClientHandler.ClientCertificateOptions = ClientCertificateOption.Manual;
        httpClientHandler.ServerCertificateCustomValidationCallback = (_, _, _, _) => true;
        return new HttpClient(httpClientHandler)
        {
            Timeout = connectionTimeout
        };
    }
}