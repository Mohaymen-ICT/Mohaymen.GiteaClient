using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.Gitea.PullRequest.MergePullRequest.Context;

internal record MergePullRequestRequest
{
    [JsonProperty("Do")]
    public required string MergeType { get; init; }
    
    [JsonProperty("delete_branch_after_merge")]
    public bool DeleteBranchAfterMerge { get; init; }
    
    [JsonProperty("force_merge")]
    public bool ForceMerge { get; init; }
    
    [JsonProperty("merge_when_checks_succeed")]
    public bool MergeWhenChecksSucceed { get; init; }
    
    [JsonProperty("MergeTitleField")]
    public string? MergeTitle { get; init; }
    
    [JsonProperty("MergeMessageField")]
    public string? MergeMessage { get; init; }
}