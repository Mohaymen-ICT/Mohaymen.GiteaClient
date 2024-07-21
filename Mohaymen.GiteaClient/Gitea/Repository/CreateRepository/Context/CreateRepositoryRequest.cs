using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Context;

internal class CreateRepositoryRequest
{
    [JsonProperty("default_branch")] 
    public required string DefaultBranch { get; init; }

    [JsonProperty("name")]
    public required string Name { get; init; }

    [JsonProperty("private")]
    public bool IsPrivateBranch { get; init; }
}