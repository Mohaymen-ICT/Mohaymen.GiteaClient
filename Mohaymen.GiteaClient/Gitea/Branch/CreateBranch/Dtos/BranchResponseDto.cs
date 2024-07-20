using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Dtos;

public class BranchResponseDto
{
    [JsonProperty("name")] 
    public required string BranchName { get; init; }
}