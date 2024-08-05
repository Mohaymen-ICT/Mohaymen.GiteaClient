﻿using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Dtos.Response;

public class CommitResponseDto
{
    [JsonProperty("sha")]
    public required string Sha { get; init; }
    
    [JsonProperty("url")]
    public required string CommitUrl { get; init; }
}