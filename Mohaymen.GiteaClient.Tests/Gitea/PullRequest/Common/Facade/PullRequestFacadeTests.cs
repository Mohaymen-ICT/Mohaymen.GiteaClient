using MediatR;
using Mohaymen.GiteaClient.Gitea.PullRequest.Common.Facade;
using Mohaymen.GiteaClient.Gitea.PullRequest.Common.Facade.Abstractions;
using Mohaymen.GiteaClient.Gitea.PullRequest.CreatePullRequest.Commands;
using Mohaymen.GiteaClient.Gitea.PullRequest.CreatePullRequest.Dtos;
using NSubstitute;
using Xunit;

namespace Mohaymen.GiteaClient.Tests.Gitea.PullRequest.Common.Facade;

public class PullRequestFacadeTests
{
    private readonly IMediator _mediator;
    private readonly IPullRequestFacade _sut;

    public PullRequestFacadeTests()
    {
        _mediator = Substitute.For<IMediator>();
        _sut = new PullRequestFacade(_mediator);
    }

    [Fact]
    public async Task CreatePullRequestAsync_ShouldCallSend_WhenAllFieldsAreProvided()
    {
        // Arrange
        const string repositoryName = "repo";
        const string headBranch = "head";
        const string baseBranch = "base";
        const string body = "body";
        const string title = "title";
        const string assignee = "assignee";
        var assignees = new List<string>
        {
            "assignee1",
            "assignee2"
        };
        var commandDto = new CreatePullRequestCommandDto
        {
            RepositoryName = repositoryName,
            HeadBranch = headBranch,
            BaseBranch = baseBranch,
            Body = body,
            Title = title,
            Assignee = assignee,
            Assignees = assignees
        };

        // Act
        await _sut.CreatePullRequestAsync(commandDto, default);

        // Assert
        await _mediator.Received(1).Send(Arg.Is<CreatePullRequestCommand>(x => x.RepositoryName == repositoryName
                                                                               && x.HeadBranch == headBranch
                                                                               && x.BaseBranch == baseBranch
                                                                               && x.Body == body
                                                                               && x.Title == title
                                                                               && x.Assignee == assignee
                                                                               && x.Assignees!.SequenceEqual(assignees)));
    }

    [Fact]
    public async Task CreatePullRequestAsync_ShouldCallSend_WhenOptionalFieldsAreNotProvided()
    {
        // Arrange
        const string repositoryName = "repo";
        const string title = "title";
        const string headBranch = "head";
        const string baseBranch = "base";
        var commandDto = new CreatePullRequestCommandDto
        {
            RepositoryName = repositoryName,
            Title = title,
            HeadBranch = headBranch,
            BaseBranch = baseBranch
        };

        // Act
        await _sut.CreatePullRequestAsync(commandDto, default);

        // Assert
        await _mediator.Received(1).Send(Arg.Is<CreatePullRequestCommand>(x => x.RepositoryName == repositoryName));
    }
}