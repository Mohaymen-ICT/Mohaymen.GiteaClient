using System.Net.Http;

namespace Mohaymen.GitClient.APICall.Domain;

internal sealed class HttpRestApiDto<TRequestBody>
{
    public TRequestBody BodyDto { get; set; }
    public HttpMethod HttpMethod { get; set; }
}