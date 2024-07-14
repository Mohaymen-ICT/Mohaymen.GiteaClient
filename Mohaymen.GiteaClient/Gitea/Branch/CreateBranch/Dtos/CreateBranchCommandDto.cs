namespace Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Dtos;

public class CreateBranchCommandDto
{
    public string NewBranchName { get; init; }
    public string OldReferenceName { get; init; }
}