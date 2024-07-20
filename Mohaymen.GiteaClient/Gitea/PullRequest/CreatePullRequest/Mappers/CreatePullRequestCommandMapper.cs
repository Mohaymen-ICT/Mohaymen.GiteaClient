using System;
using Mohaymen.GiteaClient.Gitea.PullRequest.CreatePullRequest.Commands;
using Mohaymen.GiteaClient.Gitea.PullRequest.CreatePullRequest.Dtos;

namespace Mohaymen.GiteaClient.Gitea.PullRequest.CreatePullRequest.Mappers;

internal static class CreatePullRequestCommandMapper
{
    internal static CreatePullRequestCommand ToCreatePullRequestCommand(this CreatePullRequestCommandDto createPullRequestCommandDto)
    {
        ArgumentNullException.ThrowIfNull(createPullRequestCommandDto);

        return new CreatePullRequestCommand
        {
            RepositoryName = createPullRequestCommandDto.RepositoryName,
            HeadBranch = createPullRequestCommandDto.HeadBranch,
            BaseBranch = createPullRequestCommandDto.BaseBranch,
            Body = createPullRequestCommandDto.Body,
            Title = createPullRequestCommandDto.Title,
            Assignee = createPullRequestCommandDto.Assignee,
            Assignees = createPullRequestCommandDto.Assignees
        };
    }
}