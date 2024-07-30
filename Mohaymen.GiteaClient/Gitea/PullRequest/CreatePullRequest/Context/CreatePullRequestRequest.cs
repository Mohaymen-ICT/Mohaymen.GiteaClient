using System.Collections.Generic;
using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.Gitea.PullRequest.CreatePullRequest.Context;

internal class CreatePullRequestRequest
{
    [JsonProperty("title")] 
    public required string Title { get; init; }

    [JsonProperty("head")]
    public required string HeadBranch { get; init; }

    [JsonProperty("base")]
    public required string BaseBranch { get; init; }

    [JsonProperty("body")] 
    public string? Body { get; init; }

    [JsonProperty("assignee")]
    public string? Assignee { get; init; }

    [JsonProperty("assignees")]
    public List<string> Assignees { get; init; } = []; //TODO list or array ?
}