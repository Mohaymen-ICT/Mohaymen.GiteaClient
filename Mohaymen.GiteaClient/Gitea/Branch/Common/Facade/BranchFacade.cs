using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Mohaymen.GiteaClient.Commons.Observability.Abstraction;
using Mohaymen.GiteaClient.Gitea.Branch.Common.Dtos;
using Mohaymen.GiteaClient.Gitea.Branch.Common.Facade.Abstractions;
using Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Dtos;
using Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Mappers;
using Mohaymen.GiteaClient.Gitea.Branch.GetBranchList.Dtos;
using Mohaymen.GiteaClient.Gitea.Branch.GetBranchList.Mappers;
using Refit;

namespace Mohaymen.GiteaClient.Gitea.Branch.Common.Facade;

internal class BranchFacade : IBranchFacade
{
	private readonly IMediator _mediator;
	private readonly ITraceInstrumentation _traceInstrumentation;

	public BranchFacade(IMediator mediator, ITraceInstrumentation traceInstrumentation)
	{
		_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
		_traceInstrumentation = traceInstrumentation ?? throw new ArgumentNullException(nameof(traceInstrumentation));
	}

	public async Task<ApiResponse<BranchResponseDto>> CreateBranchAsync(CreateBranchCommandDto createBranchCommandDto,
		CancellationToken cancellationToken)
	{
		using var activity = _traceInstrumentation.ActivitySource.StartActivity("CreateBranch", ActivityKind.Internal);
		var command = createBranchCommandDto.ToCreateBranchCommand();
		return await _mediator.Send(command, cancellationToken);
	}

	public async Task<ApiResponse<List<BranchResponseDto>>> GetBranchListAsync(GetBranchListCommandDto getBranchListCommandDto,
		CancellationToken cancellationToken)
	{
		using var activity = _traceInstrumentation.ActivitySource.StartActivity("GetBranchList", ActivityKind.Internal);
		var command = getBranchListCommandDto.ToGetBranchListCommand();
		return await _mediator.Send(command, cancellationToken);
	}
}
