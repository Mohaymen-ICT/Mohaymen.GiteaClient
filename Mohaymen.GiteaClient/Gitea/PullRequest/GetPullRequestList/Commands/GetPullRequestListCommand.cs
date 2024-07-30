using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using Mohaymen.GiteaClient.Core.Configs;
using Mohaymen.GiteaClient.Gitea.PullRequest.Common.ApiCall.Abstractions;
using Mohaymen.GiteaClient.Gitea.PullRequest.Common.Enums;
using Mohaymen.GiteaClient.Gitea.PullRequest.GetPullRequestList.Dtos;
using Mohaymen.GiteaClient.Gitea.PullRequest.GetPullRequestList.Mappers;
using Refit;

namespace Mohaymen.GiteaClient.Gitea.PullRequest.GetPullRequestList.Commands;

public class GetPullRequestListCommand : IRequest<ApiResponse<List<GetPullRequestListResponseDto>>>
{
    public required string RepositoryName { get; init; } 
    public PullRequestState State { get; init; }
    public SortCriteria SortBy { get; init; }
    public List<int> LabelIds { get; init; } = [];
}

internal class GetPullRequestListCommandHandler : IRequestHandler<GetPullRequestListCommand, ApiResponse<List<GetPullRequestListResponseDto>>>
{
    private readonly IPullRequestRestClient _pullRequestRestClient;
    private readonly IOptions<GiteaApiConfiguration> _options;

    public GetPullRequestListCommandHandler(IPullRequestRestClient pullRequestRestClient,
        IOptions<GiteaApiConfiguration> options)
    {
        _pullRequestRestClient = pullRequestRestClient ?? throw new ArgumentNullException(nameof(pullRequestRestClient));
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }
    
    public async Task<ApiResponse<List<GetPullRequestListResponseDto>>> Handle(GetPullRequestListCommand command, 
        CancellationToken cancellationToken)
    {
        var getPullRequestListRequest = command.ToGetPullRequestListRequest();
        var owner = _options.Value.RepositoriesOwner;
        return await _pullRequestRestClient.GetPullRequestListAsync(owner, command.RepositoryName, getPullRequestListRequest, cancellationToken)
            .ConfigureAwait(false);
    }
}