using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.IntegrationTests.Common.Models.Responses;

public class FileMetadataResponse
{
    [JsonProperty("name")]
    public required string FileName { get; init; }
    
    [JsonProperty("path")]
    public required string FilePath { get; init; }
    
    [JsonProperty("content")]
    public required string Content { get; init; }
}