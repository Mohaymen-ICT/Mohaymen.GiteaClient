using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Dtos.Response;

public class CreateCommitResponseDto
{
    [JsonProperty("sha")]
    public required string Sha { get; init; }
    
    [JsonProperty("url")]
    public required string CommitUrl { get; init; }
}