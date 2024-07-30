using System.Collections.Generic;
using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.Gitea.PullRequest.GetPullRequestList.Context;

internal record GetPullRequestListRequest
{
    [JsonProperty("state")] 
    public string? State { get; init; }
    
    [JsonProperty("sort")] 
    public string? SortBy { get; init; }

    [JsonProperty("labels")]
    public List<int> LabelIds { get; init; } = [];
}