using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.Gitea.PullRequest.Common.Models;

public class User
{
    [JsonProperty("login_name")] 
    public string? Name { get; init; }
    
    [JsonProperty("email")] 
    public required string Email { get; init; }
    
    [JsonProperty("full_name")] 
    public string? FullName { get; init; }
}