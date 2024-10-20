using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Mohaymen.GiteaClient.Gitea.PullRequest.CreatePullRequest.Dtos;
using Mohaymen.GiteaClient.Gitea.PullRequest.GetPullRequestList.Dtos;
using Mohaymen.GiteaClient.Gitea.PullRequest.MergePullRequest.Dtos;
using Refit;

namespace Mohaymen.GiteaClient.Gitea.PullRequest.Common.Facade.Abstractions;

public interface IPullRequestFacade
{
    Task<ApiResponse<CreatePullRequestResponseDto>> CreatePullRequestAsync(
        CreatePullRequestCommandDto createPullRequestCommandDto, 
        CancellationToken cancellationToken);
    
    Task<ApiResponse<List<GetPullRequestListResponseDto>>> GetPullRequestListAsync(
        GetPullRequestListCommandDto getPullRequestListCommandDto,
        CancellationToken cancellationToken);
    
    Task<ApiResponse<Unit>> MergePullRequestAsync(
        MergePullRequestCommandDto mergePullRequestCommandDto,
        CancellationToken cancellationToken);
}