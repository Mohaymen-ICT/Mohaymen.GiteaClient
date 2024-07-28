using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.IntegrationTests.Common.Models.Requests;

internal class CreateFileRequest
{
    [JsonProperty("content")]
    public required string Content { get; init; }
    
    [JsonProperty("message")]
    public required string CommitMessage { get; init; }
    
    [JsonProperty("branch")]
    public required string BranchName { get; init; }
}