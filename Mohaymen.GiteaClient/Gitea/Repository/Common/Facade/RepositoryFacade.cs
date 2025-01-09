using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Mohaymen.GiteaClient.Commons.Observability.Abstraction;
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
	private readonly ITraceInstrumentation _traceInstrumentation;

	public RepositoryFacade(IMediator mediator, ITraceInstrumentation traceInstrumentation)
	{
		_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
		_traceInstrumentation = traceInstrumentation ?? throw new ArgumentNullException(nameof(traceInstrumentation));
	}

	public async Task<ApiResponse<CreateRepositoryResponseDto>> CreateRepositoryAsync(CreateRepositoryCommandDto createRepositoryCommandDto,
		CancellationToken cancellationToken)
	{
		using var activity = _traceInstrumentation.ActivitySource.StartActivity("CreateRepository", ActivityKind.Internal);
		var command = CreateRepositoryCommandMapper.Map(createRepositoryCommandDto);
		return await _mediator.Send(command, cancellationToken);
	}

	public async Task<ApiResponse<SearchRepositoryResponseDto>> SearchRepositoryAsync(SearchRepositoryQueryDto searchRepositoryQueryDto, CancellationToken cancellationToken)
	{
		using var activity = _traceInstrumentation.ActivitySource.StartActivity("SearchRepository", ActivityKind.Internal);
		var query = SearchRepositoryQueryMapper.Map(searchRepositoryQueryDto);
		return await _mediator.Send(query, cancellationToken);
	}

	public async Task<ApiResponse<DeleteRepositoryResponseDto>> DeleteRepositoryAsync(DeleteRepositoryCommandDto deleteRepositoryCommandDto, CancellationToken cancellationToken)
	{
		using var activity = _traceInstrumentation.ActivitySource.StartActivity("DeleteRepository", ActivityKind.Internal);
		var command = DeleteRepositoryCommandMapper.Map(deleteRepositoryCommandDto);
		return await _mediator.Send(command, cancellationToken);
	}
}
