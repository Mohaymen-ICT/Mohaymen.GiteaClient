using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.Gitea.PullRequest.Common.Models;

public class GiteaBranch
{
    [JsonProperty("label")] 
    public required string Name { get; init; }
}