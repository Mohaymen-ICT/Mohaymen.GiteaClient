namespace Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Dtos;

public class CreateBranchCommandDto
{
    public required string RepositoryName { get; init; }
    public required string NewBranchName { get; init; }
    public required string OldReferenceName { get; init; }
}