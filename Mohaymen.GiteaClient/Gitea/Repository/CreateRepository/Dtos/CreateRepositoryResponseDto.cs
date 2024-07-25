using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Dtos;

public sealed class CreateRepositoryResponseDto
{
    [JsonProperty("id")] 
    public long RepositoryId { get; init; }
    
    [JsonProperty("name")] 
    public required string RepositoryName { get; init; }
}