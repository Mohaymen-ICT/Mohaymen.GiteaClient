using System.Collections.Generic;
using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Dtos.Response;

public class CreateCommandResponseDto
{
    [JsonProperty("files")]
    public required List<FileResponseDto> FileResponseDtos { get; init; }
    
    [JsonProperty("commit")]
    public required CreateCommitResponseDto CreateCommitResponseDto { get; init; }
}