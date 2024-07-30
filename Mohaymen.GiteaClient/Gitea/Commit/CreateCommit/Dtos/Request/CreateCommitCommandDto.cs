using System.Collections.Generic;

namespace Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Dtos.Request;

public class CreateCommitCommandDto
{
    public required string RepositoryName { get; init; }
    
    public required string BranchName { get; init; }
    
    public required string CommitMessage { get; init; }
    
    public required List<FileCommitDto> FileDtos { get; init; }
}