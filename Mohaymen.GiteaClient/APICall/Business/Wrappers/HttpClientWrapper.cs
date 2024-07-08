using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Mohaymen.GiteaClient.APICall.Business.Wrappers.Abstractions;

namespace Mohaymen.GiteaClient.APICall.Business.Wrappers;

internal sealed class HttpClientWrapper : IHttpClientWrapper
{
    public async Task<HttpResponseMessage> SendAsync(HttpClient httpClient,
        HttpRequestMessage httpRequestMessage,
        CancellationToken cancellationToken)
    {
        return await httpClient.SendAsync(httpRequestMessage, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
    }
}