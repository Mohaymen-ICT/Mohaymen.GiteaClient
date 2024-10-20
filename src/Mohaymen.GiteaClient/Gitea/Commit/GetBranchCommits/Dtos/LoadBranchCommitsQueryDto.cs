namespace Mohaymen.GiteaClient.Gitea.Commit.GetBranchCommits.Dtos;

public sealed class LoadBranchCommitsQueryDto
{
    public required string RepositoryName { get; init; }
    public required string BranchName { get; init; }
    public int Page { get; init; } = 1;
    public int Limit { get; init; } = 10;
}