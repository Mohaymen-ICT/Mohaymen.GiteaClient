using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Dtos;

public sealed class CreateRepositoryResponseDto
{
    [JsonProperty("id")] 
    public long RepositoryId { get; init; }
    
    [JsonProperty("name")] 
    public string RepositoryName { get; init; }
}