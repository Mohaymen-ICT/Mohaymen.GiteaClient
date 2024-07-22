using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Mohaymen.GiteaClient.Gitea.Repository.Common.Facade.Abstractions;
using Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Dtos;
using Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Mappers;
using Refit;

namespace Mohaymen.GiteaClient.Gitea.Repository.Common.Facade;

internal class RepositoryFacade : IRepositoryFacade
{
    private readonly IMediator _mediator;

    public RepositoryFacade(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }


    public async Task<ApiResponse<CreateRepositoryResponseDto>> CreateRepositoryAsync(CreateRepositoryCommandDto createRepositoryCommandDto,
        CancellationToken cancellationToken)
    {
        var command = CreateRepositoryCommandMapper.Map(createRepositoryCommandDto);
        return await _mediator.Send(command, cancellationToken);
    }
}