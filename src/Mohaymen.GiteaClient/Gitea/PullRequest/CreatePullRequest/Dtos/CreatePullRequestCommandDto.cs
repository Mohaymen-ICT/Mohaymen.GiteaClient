using System.Collections.Generic;

namespace Mohaymen.GiteaClient.Gitea.PullRequest.CreatePullRequest.Dtos;

public class CreatePullRequestCommandDto
{
    public required string RepositoryName { get; init; }
    public required string Title { get; init; }
    public required string HeadBranch { get; init; }
    public required string BaseBranch { get; init; }
    public string? Body { get; init; }
    public string? Assignee { get; init; }
    public List<string> Assignees { get; init; } = []; //TODO list or array ?
}