using System.Collections.Generic;
using Mohaymen.GiteaClient.Gitea.PullRequest.Common.Enums;

namespace Mohaymen.GiteaClient.Gitea.PullRequest.GetPullRequestList.Dtos;

public class GetPullRequestListCommandDto
{
    public required string RepositoryName { get; init; } 
    public PullRequestState State { get; init; }
    public SortCriteria SortBy { get; init; }
    public List<int>? LabelIds { get; init; }
}