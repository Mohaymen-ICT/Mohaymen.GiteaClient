using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.Gitea.Repository.SearchRepository.Context;

internal class SearchRepositoryRequest
{
    [JsonProperty("q")]
    public required string Query { get; init; }
}