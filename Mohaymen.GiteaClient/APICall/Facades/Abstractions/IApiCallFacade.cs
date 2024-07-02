using System.Threading;
using System.Threading.Tasks;
using Mohaymen.GitClient.APICall.Domain;

namespace Mohaymen.GitClient.APICall.Facades.Abstractions;

internal interface IApiCallFacade
{
    Task<HttpResponseDto<TResponseDto>> SendAsync<TRequest, TResponseDto>(HttpRestApiDto<TRequest> httpRestApiDto,
        CancellationToken cancellationToken = default);
}