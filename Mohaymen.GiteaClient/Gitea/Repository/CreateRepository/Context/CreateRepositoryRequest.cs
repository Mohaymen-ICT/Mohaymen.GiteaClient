using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Context;

internal class CreateRepositoryRequest
{
    [JsonProperty("default_branch")] 
    public string DefaultBranch { get; init; }

    [JsonProperty("name")] public string Name { get; init; }

    [JsonProperty("private")] public bool IsPrivateBranch { get; init; }
}