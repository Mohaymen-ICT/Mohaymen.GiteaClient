using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Mohaymen.GiteaClient.APICall.Business.Wrappers.Abstractions;

internal interface IHttpClientWrapper
{
    Task<HttpResponseMessage> SendAsync(HttpClient httpClient, HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken);
}