using System.Collections.Generic;
using Mohaymen.GiteaClient.Gitea.Commit.Common.Dtos;
using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Dtos.Response;

public class CommitResponseDto
{
    [JsonProperty("sha")]
    public required string Sha { get; init; }
    
    [JsonProperty("url")]
    public required string CommitUrl { get; init; }
    
    [JsonProperty("parents")]
    public required List<ParentCommitDto> ParentCommitDtos { get; init; }
}