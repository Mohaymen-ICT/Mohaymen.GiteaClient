using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Mohaymen.GiteaClient.Gitea.PullRequest.Common.Facade.Abstractions;
using Mohaymen.GiteaClient.Gitea.PullRequest.CreatePullRequest.Dtos;
using Mohaymen.GiteaClient.Gitea.PullRequest.CreatePullRequest.Mappers;
using Mohaymen.GiteaClient.Gitea.PullRequest.GetPullRequestList.Dtos;
using Mohaymen.GiteaClient.Gitea.PullRequest.GetPullRequestList.Mappers;
using Refit;

namespace Mohaymen.GiteaClient.Gitea.PullRequest.Common.Facade;

internal class PullRequestFacade : IPullRequestFacade
{
    private readonly IMediator _mediator;

    public PullRequestFacade(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task<ApiResponse<CreatePullRequestResponseDto>> CreatePullRequestAsync(CreatePullRequestCommandDto createPullRequestCommandDto, 
        CancellationToken cancellationToken)
    {
        var command = createPullRequestCommandDto.ToCreatePullRequestCommand();
        return await _mediator.Send(command, cancellationToken);
    }

    public async Task<ApiResponse<List<GetPullRequestListResponseDto>>> GetPullRequestListAsync(GetPullRequestListCommandDto getPullRequestListCommandDto,
        CancellationToken cancellationToken)
    {
        var command = getPullRequestListCommandDto.Map();
        return await _mediator.Send(command, cancellationToken);
    }
}