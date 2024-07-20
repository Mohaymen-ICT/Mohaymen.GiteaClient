using System.Threading.Tasks;
using Mohaymen.GiteaClient.Core.ApiCall.Abstractions;
using Mohaymen.GiteaClient.Gitea.Commit.GetBranchCommits.Dtos;
using Refit;

namespace Mohaymen.GiteaClient.Gitea.Commit.Common.ApiCall;

internal interface ICommitRestClient : IRefitClientInterface
{
    [Get("/repos/{owner}/{repositoryName}/commits?sha={branchName}&page={page}&limit={limit}")]
    Task<ApiResponse<LoadBranchCommitsResponseDto>> LoadBranchCommitsAsync(string owner,
        string repositoryName,
        string branchName,
        int page,
        int limit);
}