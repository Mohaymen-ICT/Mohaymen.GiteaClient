using System.Collections.Generic;
using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Context;

internal sealed class CreateCommitRequest
{
    [JsonProperty("branch")]
    public required string BranchName { get; init; }
    
    [JsonProperty("message")]
    public required string CommitMessage { get; init; }
    
    [JsonProperty("files")]
    public required List<FileCommitRequest> FileCommitRequests { get; init; }
}