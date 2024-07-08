using System.Collections.Specialized;
using System.Net.Http;

namespace Mohaymen.GiteaClient.APICall.Business.HttpRequestBuilder.Abstractions;

internal interface IHttpRequestMessageFactory
{
    HttpRequestMessage CreateHttpRequestMessage(string url,
        HttpMethod httpMethod,
        string jsonBody,
        NameValueCollection requestHeaders,
        NameValueCollection contentHeaders);
}