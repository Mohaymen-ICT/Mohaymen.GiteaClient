﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Mohaymen.GiteaClient.Core.ApiCall.Abstractions;
using Mohaymen.GiteaClient.Gitea.PullRequest.CreatePullRequest.Context;
using Mohaymen.GiteaClient.Gitea.PullRequest.CreatePullRequest.Dtos;
using Mohaymen.GiteaClient.Gitea.PullRequest.GetPullRequestList.Context;
using Mohaymen.GiteaClient.Gitea.PullRequest.GetPullRequestList.Dtos;
using Mohaymen.GiteaClient.Gitea.PullRequest.MergePullRequest.Context;
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
    
    [Get("/repos/{owner}/{repo}/pulls")]
    Task<ApiResponse<List<GetPullRequestListResponseDto>>> GetPullRequestListAsync(
        [AliasAs("owner")] string owner,
        [AliasAs("repo")] string repositoryName,
        [Body] GetPullRequestListRequest getPullRequestListRequest,
        CancellationToken cancellationToken);
    
    [Post("/repos/{owner}/{repo}/pulls/{index}/merge")]
    Task<ApiResponse<Unit>> MergePullRequestAsync(
        [AliasAs("owner")] string owner,
        [AliasAs("repo")] string repositoryName,
        [AliasAs("index")] int index,
        [Body] MergePullRequestRequest mergePullRequestRequest,
        CancellationToken cancellationToken);
}