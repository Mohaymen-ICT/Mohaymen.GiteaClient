using System.Collections.Generic;

namespace Mohaymen.GiteaClient.Gitea.PullRequest.CreatePullRequest.Dtos;

public class CreatePullRequestCommandDto
{
    public required string RepositoryName { get; init; } 
    public string? HeadBranch { get; init; }
    public string? BaseBranch { get; init; }
    public string? Body { get; init; }
    public string? Title { get; init; }
    public string? Assignee { get; init; }
    public List<string> Assignees { get; init; } = []; //TODO list or array ?
}