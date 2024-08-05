using Mohaymen.GiteaClient.Gitea.PullRequest.Common.Enums;

namespace Mohaymen.GiteaClient.Gitea.PullRequest.MergePullRequest.Dtos;

public record MergePullRequestCommandDto
{
    public required string RepositoryName { get; init; }
    public required int Index { get; init; }
    public required MergeType MergeType { get; init; }
    public bool DeleteBranchAfterMerge { get; init; }
    public bool ForceMerge { get; init; }
    public bool MergeWhenChecksSucceed { get; init; }
    public string? MergeTitle { get; init; }
    public string? MergeMessage { get; init; }
}