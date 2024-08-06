namespace Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Dtos.Request;

public class FileCommitDto
{
    public required string Path { get; init; }
    public required string Content { get; init; }
    public string? FileHash { get; init; }
    public required CommitActionDto CommitActionDto { get; init; }
}