using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Mohaymen.GiteaClient.Gitea.PullRequest.CreatePullRequest.Dtos;
using Mohaymen.GiteaClient.Gitea.PullRequest.GetPullRequestList.Dtos;
using Refit;

namespace Mohaymen.GiteaClient.Gitea.PullRequest.Common.Facade.Abstractions;

public interface IPullRequestFacade
{
    Task<ApiResponse<CreatePullRequestResponseDto>> CreatePullRequestAsync(
        CreatePullRequestCommandDto createPullRequestCommandDto, 
        CancellationToken cancellationToken);
    
    Task<ApiResponse<List<GetPullRequestListResponseDto>>> GetPullRequestListAsync(GetPullRequestListCommandDto getPullRequestListCommandDto,
        CancellationToken cancellationToken);
}