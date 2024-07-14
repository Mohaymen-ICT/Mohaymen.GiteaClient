namespace Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Dtos;

public class CreateBranchCommandDto
{
    public string Owner { get; init; }
    public string RepositoryName { get; init; }
    public string NewBranchName { get; init; }
    public string OldReferenceName { get; init; }
}