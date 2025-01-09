using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Mohaymen.GiteaClient.Commons.Observability.Abstraction;
using Mohaymen.GiteaClient.Gitea.PullRequest.Common.Facade.Abstractions;
using Mohaymen.GiteaClient.Gitea.PullRequest.CreatePullRequest.Dtos;
using Mohaymen.GiteaClient.Gitea.PullRequest.CreatePullRequest.Mappers;
using Mohaymen.GiteaClient.Gitea.PullRequest.GetPullRequestList.Dtos;
using Mohaymen.GiteaClient.Gitea.PullRequest.GetPullRequestList.Mappers;
using Mohaymen.GiteaClient.Gitea.PullRequest.MergePullRequest.Dtos;
using Mohaymen.GiteaClient.Gitea.PullRequest.MergePullRequest.Mappers;
using Refit;

namespace Mohaymen.GiteaClient.Gitea.PullRequest.Common.Facade;

internal class PullRequestFacade : IPullRequestFacade
{
	private readonly IMediator _mediator;
	private readonly ITraceInstrumentation _traceInstrumentation;

	public PullRequestFacade(IMediator mediator, ITraceInstrumentation traceInstrumentation)
	{
		_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
		_traceInstrumentation = traceInstrumentation ?? throw new ArgumentNullException(nameof(traceInstrumentation));
	}

	public async Task<ApiResponse<CreatePullRequestResponseDto>> CreatePullRequestAsync(CreatePullRequestCommandDto createPullRequestCommandDto,
		CancellationToken cancellationToken)
	{
		using var activity = _traceInstrumentation.ActivitySource.StartActivity("CreatePullRequest", ActivityKind.Internal);
		var command = createPullRequestCommandDto.ToCreatePullRequestCommand();
		return await _mediator.Send(command, cancellationToken);
	}

	public async Task<ApiResponse<List<GetPullRequestListResponseDto>>> GetPullRequestListAsync(GetPullRequestListCommandDto getPullRequestListCommandDto,
		CancellationToken cancellationToken)
	{
		using var activity = _traceInstrumentation.ActivitySource.StartActivity("GetPullRequestList", ActivityKind.Internal);
		var command = getPullRequestListCommandDto.Map();
		return await _mediator.Send(command, cancellationToken);
	}

	public async Task<ApiResponse<Unit>> MergePullRequestAsync(MergePullRequestCommandDto mergePullRequestCommandDto,
		CancellationToken cancellationToken)
	{
		using var activity = _traceInstrumentation.ActivitySource.StartActivity("MergePullRequest", ActivityKind.Internal);
		var command = mergePullRequestCommandDto.Map();
		return await _mediator.Send(command, cancellationToken);
	}
}
