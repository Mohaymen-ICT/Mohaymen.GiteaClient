using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Dtos.Response;

public class FileResponseDto
{
    [JsonProperty("path")]
    public required string Path { get; init; }
    
    [JsonProperty("sha")]
    public required string FileHash { get; init; }
    
    [JsonProperty("download_url")]
    public required string FileDownloadUrl { get; init; }
}