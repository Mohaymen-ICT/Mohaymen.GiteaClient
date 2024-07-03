using System;
using System.Collections.Specialized;
using System.Net.Http;
using Mohaymen.GitClient.APICall.Business.HttpRequestBuilder.Abstractions;

namespace Mohaymen.GitClient.APICall.Business.HttpRequestBuilder;

internal sealed class HttpRequestMessageFactory : IHttpRequestMessageFactory
{
    public HttpRequestMessage CreateHttpRequestMessage(string url,
        HttpMethod httpMethod,
        string jsonBody,
        NameValueCollection requestHeaders,
        NameValueCollection contentHeaders)
    {
        var httpRequestMessage = new HttpRequestMessage()
        {
            Content = new StringContent(jsonBody),
            RequestUri = new Uri(url),
            Method = httpMethod
        };
        SetHeaders(httpRequestMessage, requestHeaders, contentHeaders);
        return httpRequestMessage;
    }

    private static void SetHeaders(HttpRequestMessage httpRequestMessage, NameValueCollection requestHeaders,
        NameValueCollection contentHeaders)
    {
        foreach (var contentHeaderName in contentHeaders.AllKeys)
        {
            if (httpRequestMessage.Content.Headers.Contains(contentHeaderName))
            {
                httpRequestMessage.Content.Headers.Remove(contentHeaderName);
            }

            httpRequestMessage.Content.Headers.Add(contentHeaderName, contentHeaders.GetValues(contentHeaderName));
        }

        foreach (var requestHeaderName in requestHeaders.AllKeys)
        {
            if (httpRequestMessage.Headers.Contains(requestHeaderName))
            {
                httpRequestMessage.Headers.Remove(requestHeaderName);
            }

            httpRequestMessage.Headers.Add(requestHeaderName, requestHeaders.GetValues(requestHeaderName));
        }
    }
}