using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Context;

public class CreateBranchRequest
{
    [JsonProperty("new_branch_name")]
    public string NewBranchName { get; init; }
    
    [JsonProperty("old_ref_name")]
    public string OldReferenceName { get; init; }
}