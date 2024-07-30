using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Dtos.Response;

public class CreateCommitResponseDto
{
    [JsonProperty("commit")]
    public required CommitResponseDto CommitResponseDto { get; init; }
}