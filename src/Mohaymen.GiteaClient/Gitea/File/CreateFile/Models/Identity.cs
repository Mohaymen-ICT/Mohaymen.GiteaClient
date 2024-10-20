using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.Gitea.File.CreateFile.Models;

public class Identity
{
    [JsonProperty("email")]
    public required string Email { get; init; }
    
    [JsonProperty("name")]
    public required string Name { get; init; }
}