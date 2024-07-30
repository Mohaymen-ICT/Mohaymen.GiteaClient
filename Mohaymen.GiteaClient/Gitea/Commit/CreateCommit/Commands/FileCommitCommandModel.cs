namespace Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Commands;

internal sealed record FileCommitCommandModel
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
