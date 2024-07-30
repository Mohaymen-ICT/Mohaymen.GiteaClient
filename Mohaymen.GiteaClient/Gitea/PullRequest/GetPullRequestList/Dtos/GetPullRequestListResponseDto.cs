using System.Collections.Generic;
using Mohaymen.GiteaClient.Gitea.PullRequest.Common.Models;
using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.Gitea.PullRequest.GetPullRequestList.Dtos;

public class GetPullRequestListResponseDto
{
    [JsonProperty("title")] 
    public required string Title { get; init; }

    [JsonProperty("head")] 
    public required GiteaBranch HeadBranch { get; init; }

    [JsonProperty("base")] 
    public required GiteaBranch BaseBranch { get; init; }

    [JsonProperty("body")] 
    public string? Body { get; init; }

    [JsonProperty("assignees")] 
    public List<User> Assignees { get; init; } = [];
    
    [JsonProperty("merged")] 
    public bool Merged { get; init; }
}