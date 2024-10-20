using Mohaymen.GiteaClient.Gitea.File.CreateFile.Models;
using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.Gitea.File.CreateFile.Context;

internal record CreateFileRequest
{
    [JsonProperty("content")]
    public required string Content { get; init; }
    
    [JsonProperty("author")]
    public Identity? Author { get; init; }
    
    [JsonProperty("branch")]
    public string? BranchName { get; init; }
    
    [JsonProperty("message")]
    public string? CommitMessage { get; init; }
}