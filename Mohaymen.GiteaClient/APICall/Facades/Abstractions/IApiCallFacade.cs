using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Mohaymen.GiteaClient.APICall.Domain;

namespace Mohaymen.GiteaClient.APICall.Facades.Abstractions;

internal interface IApiCallFacade
{
    Task<GiteaResponseDto<TResponseDto>> SendAsync<TRequest, TResponseDto>(HttpRestApiDto<TRequest> httpRestApiDto,
        CancellationToken cancellationToken = default)
        where TRequest: IRequest<GiteaResponseDto<TResponseDto>>;
}