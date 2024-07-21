namespace Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Commands;

internal class FileCommitCommand
{
    public required string Path { get; init; }
    public required string Content { get; init; }
    public required CommitActionCommand CommitActionCommand { get; init; }
}

internal enum CommitActionCommand
{
    Create,
    Update,
    Delete
}
