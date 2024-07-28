using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.Gitea.PullRequest.Common.Models;

public class User
{
    public User(string email)
    {
        Email = email;
    }

    [JsonProperty("login_name")] 
    public string? Name { get; init; }
    
    [JsonProperty("email")] 
    public string? Email { get; init; }
    
    [JsonProperty("full_name")] 
    public string? FullName { get; init; }
}