using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Mohaymen.GiteaClient.Gitea.Commit.Common.Facades.Abstractions;
using Mohaymen.GiteaClient.Gitea.Commit.GetBranchCommits.Dtos;
using Mohaymen.GiteaClient.Gitea.Commit.GetBranchCommits.Mappers;
using Refit;

namespace Mohaymen.GiteaClient.Gitea.Commit.Common.Facades;

internal class CommitFacade : ICommitFacade
{
    private readonly IMediator _mediator;

    public CommitFacade(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task<ApiResponse<LoadBranchCommitsResponseDto>> LoadBranchCommitsAsync(LoadBranchCommitsQueryDto loadBranchCommitsQueryDto,
        CancellationToken cancellationToken)
    {
        var query = loadBranchCommitsQueryDto.Map();
        return await _mediator.Send(query, cancellationToken);
    }
}