using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.Gitea.PullRequest.Common.Models;

public class Branch
{
    [JsonProperty("label")] 
    public string? Name { get; init; }
}