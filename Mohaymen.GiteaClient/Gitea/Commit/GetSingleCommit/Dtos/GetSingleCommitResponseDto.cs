using System.Collections.Generic;
using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.Gitea.Commit.GetBranchCommits.Dtos;

public sealed class GetSingleCommitResponseDto
{
    [JsonProperty("url")] 
    public required string Url { get; init; }

    [JsonProperty("sha")] 
    public required string CommitSha { get; init; }

    [JsonProperty("files")] 
    public required List<FilesDto> FilesDto { get; init; }

    [JsonProperty("stats")] 
    public required StatsDto StatsDto { get; init; }
}

public sealed class FilesDto
{
    [JsonProperty("filename")] 
    public required string Filename { get; init; }

    [JsonProperty("status")] 
    public required string Status { get; init; }
}

public sealed class StatsDto
{
    [JsonProperty("total")] 
    public required int Total { get; init; }

    [JsonProperty("additions")] 
    public required int Additions { get; init; }

    [JsonProperty("deletions")] 
    public required int Deletions { get; init; }
}