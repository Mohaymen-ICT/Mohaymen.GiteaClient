using Mohaymen.GiteaClient.Gitea.File.CreateFile.Models;

namespace Mohaymen.GiteaClient.Gitea.File.CreateFile.Dtos;

public record CreateFileCommandDto
{
    public required string RepositoryName { get; init; }
    public required string FilePath { get; init; }
    public required string Content { get; init; }
    public Identity? Author { get; init; }
    public string? BranchName { get; init; }
    public string? CommitMessage { get; init; }
}