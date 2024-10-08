﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.Gitea.Repository.SearchRepository.Dtos;

public sealed class SearchRepositoryResponseDto
{
    [JsonProperty("data")]
    public required List<RepositorySearchDto> SearchResult { get; init; }
}

public class RepositorySearchDto
{
    [JsonProperty("id")]
    public int Id { get; init; }
    
    [JsonProperty("name")]
    public required string RepositoryName { get; set; }
}
