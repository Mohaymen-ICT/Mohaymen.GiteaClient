using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.Gitea.Commit.GetBranchCommits.Dtos;

public sealed class LoadBranchCommitsResponseDto
{
    [JsonProperty("url")]
    public required string Url { get; init; }
    
    [JsonProperty("sha")]
    public required string CommitSha { get; init; }
    
    [JsonProperty("commit")]
    public required CommitDto CommitDto { get; init; }
}

public sealed class CommitDto
{
    [JsonProperty("message")]
    public required string CommitMessage { get; init; }
}