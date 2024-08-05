using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.Gitea.File.GetRepositoryFile.Dtos;

public class GetFileResponseDto
{
    [JsonProperty("content")]
    public required string Content { get; set; }
    
    [JsonProperty("name")]
    public required string FileName { get; init; }
    
    [JsonProperty("path")]
    public required string FilePath { get; init; }
    
    [JsonProperty("sha")]
    public required string FileSha { get; init; }
}