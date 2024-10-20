using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.Gitea.Commit.Common.Dtos;

public sealed class ParentCommitDto
{
    [JsonProperty("sha")]
    public required string CommitSha { get; init; }
}

public sealed class CommitDto
{
    [JsonProperty("url")] 
    public required string CommitUrl { get; init; }

    [JsonProperty("message")] 
    public required string CommitMessage { get; init; }
}