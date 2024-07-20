using System;
using Mohaymen.GiteaClient.Gitea.PullRequest.CreatePullRequest.Commands;
using Mohaymen.GiteaClient.Gitea.PullRequest.CreatePullRequest.Context;

namespace Mohaymen.GiteaClient.Gitea.PullRequest.CreatePullRequest.Mappers;

internal static class CreatePullRequestRequestMapper
{
    internal static CreatePullRequestRequest ToCreatePullRequestRequest(this CreatePullRequestCommand createPullRequestCommand)
    {
        ArgumentNullException.ThrowIfNull(createPullRequestCommand);

        return new CreatePullRequestRequest
        {
            HeadBranch = createPullRequestCommand.HeadBranch,
            BaseBranch = createPullRequestCommand.BaseBranch,
            Body = createPullRequestCommand.Body,
            Title = createPullRequestCommand.Title,
            Assignee = createPullRequestCommand.Assignee,
            Assignees = createPullRequestCommand.Assignees
        };
    }
}