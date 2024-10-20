using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.Gitea.PullRequest.Common.Models;

public class BranchDto
{
    [JsonProperty("label")] 
    public required string Name { get; init; }
}