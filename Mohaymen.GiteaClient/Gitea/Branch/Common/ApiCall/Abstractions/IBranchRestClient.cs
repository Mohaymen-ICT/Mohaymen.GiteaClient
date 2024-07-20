using System.Collections.Generic;
using System.Threading.Tasks;
using Mohaymen.GiteaClient.Core.ApiCall.Abstractions;
using Mohaymen.GiteaClient.Gitea.Branch.Common.Dtos;
using Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Context;
using Refit;

namespace Mohaymen.GiteaClient.Gitea.Branch.Common.ApiCall.Abstractions;

internal interface IBranchRestClient : IRefitClientInterface
{
    [Post("/repos/{owner}/{repo}/branches")]
    Task<ApiResponse<BranchResponseDto>> CreateBranchAsync(
        [AliasAs("owner")] string owner,
        [AliasAs("repo")] string repositoryName,
        [Body] CreateBranchRequest createBranchRequest);
    
    [Get("/repos/{owner}/{repo}/branches")]
    Task<ApiResponse<List<BranchResponseDto>>> GetBranchListAsync(
        [AliasAs("owner")] string owner,
        [AliasAs("repo")] string repositoryName);
}