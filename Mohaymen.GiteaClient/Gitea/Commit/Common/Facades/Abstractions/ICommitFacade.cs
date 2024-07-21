using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Mohaymen.GiteaClient.Gitea.Commit.GetBranchCommits.Dtos;
using Refit;

namespace Mohaymen.GiteaClient.Gitea.Commit.Common.Facades.Abstractions;

public interface ICommitFacade
{
    Task<ApiResponse<List<LoadBranchCommitsResponseDto>>> LoadBranchCommitsAsync(LoadBranchCommitsQueryDto loadBranchCommitsQueryDto,
        CancellationToken cancellationToken);
}