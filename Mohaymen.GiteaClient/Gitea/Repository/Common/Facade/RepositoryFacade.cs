using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Mohaymen.GiteaClient.Gitea.Repository.Common.Facade.Abstractions;
using Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Dtos;
using Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Mappers;
using Mohaymen.GiteaClient.Gitea.Repository.DeleteRepository.Dto;
using Mohaymen.GiteaClient.Gitea.Repository.DeleteRepository.Mappers;
using Mohaymen.GiteaClient.Gitea.Repository.SearchRepository.Dtos;
using Mohaymen.GiteaClient.Gitea.Repository.SearchRepository.Mappers;
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
        var command = createRepositoryCommandDto.Map();
        return await _mediator.Send(command, cancellationToken);
    }

    public async Task<ApiResponse<SearchRepositoryResponseDto>> SearchRepositoryAsync(SearchRepositoryQueryDto searchRepositoryQueryDto,
        CancellationToken cancellationToken)
    {
        var query = searchRepositoryQueryDto.Map();
        return await _mediator.Send(query, cancellationToken);
    }

    public async Task<ApiResponse<DeleteRepositoryResponseDto>> DeleteRepositoryAsync(DeleteRepositoryCommandDto deleteRepositoryCommandDto,
        CancellationToken cancellationToken)
    {
        var command = deleteRepositoryCommandDto.Map();
        return await _mediator.Send(command, cancellationToken);
    }
    
}