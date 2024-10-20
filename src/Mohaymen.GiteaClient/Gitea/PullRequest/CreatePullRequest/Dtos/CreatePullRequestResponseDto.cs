using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.Gitea.PullRequest.CreatePullRequest.Dtos;

public class CreatePullRequestResponseDto
{
    [JsonProperty("title")] 
    public string? Title { get; init; }
    
    //TODO which fields should be added?
}