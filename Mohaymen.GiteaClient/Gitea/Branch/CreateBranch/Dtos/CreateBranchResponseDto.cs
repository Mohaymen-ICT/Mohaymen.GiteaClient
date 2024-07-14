using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Dtos;

public class CreateBranchResponseDto
{
    [JsonProperty("name")] 
    public string BranchName { get; init; }
}