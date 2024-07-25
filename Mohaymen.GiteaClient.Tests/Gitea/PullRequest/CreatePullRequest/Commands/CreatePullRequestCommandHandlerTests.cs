using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using Mohaymen.GiteaClient.Core.Configs;
using Mohaymen.GiteaClient.Gitea.PullRequest.Common.ApiCall.Abstractions;
using Mohaymen.GiteaClient.Gitea.PullRequest.CreatePullRequest.Commands;
using Mohaymen.GiteaClient.Gitea.PullRequest.CreatePullRequest.Context;
using Mohaymen.GiteaClient.Gitea.PullRequest.CreatePullRequest.Dtos;
using NSubstitute;
using Refit;
using Xunit;

namespace Mohaymen.GiteaClient.Tests.Gitea.PullRequest.CreatePullRequest.Commands;

public class CreatePullRequestCommandHandlerTests
{
    private readonly IPullRequestRestClient _pullRequestRestClient;
    private readonly IOptions<GiteaApiConfiguration> _options;
    private readonly InlineValidator<CreatePullRequestCommand> _validator;
    private readonly IRequestHandler<CreatePullRequestCommand, ApiResponse<CreatePullRequestResponseDto>> _sut;

    public CreatePullRequestCommandHandlerTests()
    {
        _pullRequestRestClient = Substitute.For<IPullRequestRestClient>();
        _options = Substitute.For<IOptions<GiteaApiConfiguration>>();
        _validator = new InlineValidator<CreatePullRequestCommand>();
        _sut = new CreatePullRequestCommandHandler(_pullRequestRestClient, _options, _validator);
    }

    [Fact]
    public async Task Handle_ShouldThrowsValidationException_WhenInputIsInvalid()
    {
        // Arrange
        _validator.RuleFor(x => x).Must(_ => false);
        var command = new CreatePullRequestCommand
        {
            RepositoryName = "repo"
        };

        // Act
        var actual = async () => await _sut.Handle(command, default);

        // Assert
        await actual.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task Handle_ShouldCallCreatePullRequestAsync_AndInputsAreValid()
    {
        // Arrange
        _validator.RuleFor(x => x).Must(_ => true);
        const string owner = "owner";
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
        var command = new CreatePullRequestCommand
        {
            RepositoryName = repositoryName,
            HeadBranch = headBranch,
            BaseBranch = baseBranch,
            Body = body,
            Title = title,
            Assignee = assignee,
            Assignees = assignees
        };
        _options.Value.Returns(new GiteaApiConfiguration
        {
            BaseUrl = "url",
            PersonalAccessToken = "token",
            RepositoriesOwner = owner
        });

        // Act
        await _sut.Handle(command, default);
        
        // Assert
        await _pullRequestRestClient.Received(1).CreatePullRequestAsync(owner,
            repositoryName,
            Arg.Is<CreatePullRequestRequest>(x => x.HeadBranch == headBranch
                                                  && x.BaseBranch == baseBranch
                                                  && x.Body == body
                                                  && x.Title == title
                                                  && x.Assignee == assignee
                                                  && x.Assignees!.SequenceEqual(assignees)),
            default);
    }
}