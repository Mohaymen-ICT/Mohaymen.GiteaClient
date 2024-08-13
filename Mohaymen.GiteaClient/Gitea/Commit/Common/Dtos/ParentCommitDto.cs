using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.Gitea.Commit.Common.Dtos;

public sealed class ParentCommitDto
{
    [JsonProperty("sha")]
    public required string CommitSha { get; init; }
}