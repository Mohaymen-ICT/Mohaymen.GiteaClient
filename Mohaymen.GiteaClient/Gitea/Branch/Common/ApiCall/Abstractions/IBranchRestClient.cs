using System.Threading.Tasks;
using Mohaymen.GiteaClient.Core.ApiCall.Abstractions;
using Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Context;
using Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Dtos;
using Refit;

namespace Mohaymen.GiteaClient.Gitea.Branch.Common.ApiCall.Abstractions;

internal interface IBranchRestClient : IRefitClientInterface
{
    [Post("/repos/{owner}/{repo}/branches")]
    Task<ApiResponse<CreateBranchResponseDto>> CreateBranchAsync(
        [AliasAs("owner")] string owner,
        [AliasAs("repo")] string repositoryName,
        [Body] CreateBranchRequest createBranchRequest);
}