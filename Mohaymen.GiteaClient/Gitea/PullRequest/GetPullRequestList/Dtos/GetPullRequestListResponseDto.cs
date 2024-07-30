using System.Collections.Generic;
using Mohaymen.GiteaClient.Gitea.PullRequest.Common.Models;
using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.Gitea.PullRequest.GetPullRequestList.Dtos;

public class GetPullRequestListResponseDto
{
    [JsonProperty("title")] 
    public string? Title { get; init; }
    
    [JsonProperty("body")] 
    public string? Body { get; init; }
    
    [JsonProperty("head")] 
    public GiteaBranch? HeadBranch { get; init; }
    
    [JsonProperty("base")] 
    public GiteaBranch? BaseBranch { get; init; }
    
    [JsonProperty("assignees")] 
    public List<User> Assignees { get; init; } = [];
    
    [JsonProperty("merged")] 
    public bool Merged { get; init; }
}