using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Mohaymen.GiteaClient.Commons.Observability.Abstraction;
using Mohaymen.GiteaClient.Gitea.Commit.Common.Facades.Abstractions;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Dtos.Request;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Dtos.Response;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Mappers;
using Mohaymen.GiteaClient.Gitea.Commit.GetBranchCommits.Dtos;
using Mohaymen.GiteaClient.Gitea.Commit.GetBranchCommits.Mappers;
using Refit;

namespace Mohaymen.GiteaClient.Gitea.Commit.Common.Facades;

internal class CommitFacade : ICommitFacade
{
	private readonly IMediator _mediator;
	private readonly ITraceInstrumentation _traceInstrumentation;

	public CommitFacade(IMediator mediator, ITraceInstrumentation traceInstrumentation)
	{
		_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
		_traceInstrumentation = traceInstrumentation ?? throw new ArgumentNullException(nameof(traceInstrumentation));
	}

	public async Task<ApiResponse<List<LoadBranchCommitsResponseDto>>> LoadBranchCommitsAsync(LoadBranchCommitsQueryDto loadBranchCommitsQueryDto,
		CancellationToken cancellationToken)
	{
		using var activity = _traceInstrumentation.ActivitySource.StartActivity("LoadBranchCommits", ActivityKind.Internal);
		var query = loadBranchCommitsQueryDto.Map();
		return await _mediator.Send(query, cancellationToken);
	}

	public async Task<ApiResponse<CreateCommitResponseDto>> CreateCommitAsync(CreateCommitCommandDto createCommitCommandDto,
		CancellationToken cancellationToken)
	{
		using var activity = _traceInstrumentation.ActivitySource.StartActivity("CreateCommit", ActivityKind.Internal);
		var command = createCommitCommandDto.MapToCommand();
		return await _mediator.Send(command, cancellationToken);
	}

	public async Task<ApiResponse<GetSingleCommitResponseDto>> GetSingleCommitAsync(
		GetSingleCommitQueryDto getSingleCommitQueryDto, CancellationToken cancellationToken)
	{
		using var activity = _traceInstrumentation.ActivitySource.StartActivity("GetSingleCommit", ActivityKind.Internal);
		var query = getSingleCommitQueryDto.Map();
		return await _mediator.Send(query, cancellationToken);
	}
}
