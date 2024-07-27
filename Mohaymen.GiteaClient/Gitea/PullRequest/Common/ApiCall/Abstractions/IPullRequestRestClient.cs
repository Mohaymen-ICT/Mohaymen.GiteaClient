using System.Threading;
using System.Threading.Tasks;
using Mohaymen.GiteaClient.Core.ApiCall.Abstractions;
using Mohaymen.GiteaClient.Gitea.PullRequest.CreatePullRequest.Context;
using Mohaymen.GiteaClient.Gitea.PullRequest.CreatePullRequest.Dtos;
using Refit;

namespace Mohaymen.GiteaClient.Gitea.PullRequest.Common.ApiCall.Abstractions;

internal interface IPullRequestRestClient : IRefitClientInterface
{
    [Post("/repos/{owner}/{repo}/pulls")]
    Task<ApiResponse<CreatePullRequestResponseDto>> CreatePullRequestAsync(
        [AliasAs("owner")] string owner,
        [AliasAs("repo")] string repositoryName,
        [Body] CreatePullRequestRequest createPullRequestRequest,
        CancellationToken cancellationToken);
}