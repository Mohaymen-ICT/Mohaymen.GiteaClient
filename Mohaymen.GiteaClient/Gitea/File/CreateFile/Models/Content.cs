using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.Gitea.File.CreateFile.Models;

public class Content
{
    [JsonProperty("name")]
    public required string FileName { get; init; }
    
    [JsonProperty("path")]
    public required string FilePath { get; init; }
    
    [JsonProperty("content")]
    public required string StringContent { get; init; }
    
    [JsonProperty("sha")]
    public required string FileSha { get; init; }
}