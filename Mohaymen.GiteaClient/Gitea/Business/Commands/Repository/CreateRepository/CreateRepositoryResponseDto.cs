using Newtonsoft.Json;

namespace Mohaymen.GitClient.Gitea.Business.Commands.Repository.CreateRepository;

public sealed class CreateRepositoryResponseDto
{
    [JsonProperty("id")]
    public long RepositoryId { get; set; }
    
    [JsonProperty("name")]
    public string RepositoryName { get; set; }
}