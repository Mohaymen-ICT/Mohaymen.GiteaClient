using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Mohaymen.GitClient.APICall.Domain;

namespace Mohaymen.GitClient.APICall.Facades.Abstractions;

internal interface IApiCallFacade
{
    Task<GiteaResponseDto<TResponseDto>> SendAsync<TRequest, TResponseDto>(HttpRestApiDto<TRequest> httpRestApiDto,
        CancellationToken cancellationToken = default)
        where TRequest: IRequest<GiteaResponseDto<TResponseDto>>;
}