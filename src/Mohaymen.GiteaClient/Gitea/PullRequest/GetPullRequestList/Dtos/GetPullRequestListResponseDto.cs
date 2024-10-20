using System.Collections.Generic;
using Mohaymen.GiteaClient.Gitea.PullRequest.Common.Models;
using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.Gitea.PullRequest.GetPullRequestList.Dtos;

public record GetPullRequestListResponseDto
{
    [JsonProperty("title")] 
    public required string Title { get; init; }

    [JsonProperty("head")] 
    public required BranchDto HeadBranchDto { get; init; }

    [JsonProperty("base")] 
    public required BranchDto BaseBranchDto { get; init; }

    [JsonProperty("body")] 
    public string Body { get; init; } = "";

    [JsonProperty("assignees")] 
    public List<User> Assignees { get; init; } = [];
    
    [JsonProperty("merged")] 
    public bool Merged { get; init; }
}