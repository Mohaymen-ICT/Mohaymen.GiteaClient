using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.Gitea.Repository.SearchRepository.Context;

internal sealed class SearchRepositoryRequest
{
    [JsonProperty("q")]
    public required string Query { get; init; }
}