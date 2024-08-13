namespace Mohaymen.GiteaClient.Gitea.Commit.GetBranchCommits.Dtos;

public sealed class GetSingleCommitQueryDto
{
    public required string RepositoryName { get; init; }
    
    public required string CommitSha { get; init; }

}