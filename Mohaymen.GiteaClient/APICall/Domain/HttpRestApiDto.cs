using System.Net.Http;

namespace Mohaymen.GiteaClient.APICall.Domain;

internal sealed class HttpRestApiDto<TRequestBody>
{
    public TRequestBody BodyDto { get; init; }
    public HttpMethod HttpMethod { get; init; }
}