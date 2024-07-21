using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using Mohaymen.GiteaClient.Core.Configs;
using Mohaymen.GiteaClient.Gitea.Commit.Common.ApiCall;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Commands;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Context;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Dtos.Response;
using NSubstitute;
using Refit;
using Xunit;

namespace Mohaymen.GiteaClient.Tests.Gitea.Commit.CreateCommit.Commands;

public class CreateCommitCommandHandlerTests
{
    private readonly InlineValidator<CreateCommitCommand> _validator;
    private readonly ICommitRestClient _commitRestClient;
    private readonly IOptions<GiteaApiConfiguration> _giteaOptions;
    private readonly IRequestHandler<CreateCommitCommand, ApiResponse<CreateCommitResponseDto>> _sut;

    public CreateCommitCommandHandlerTests()
    {
        _validator = new InlineValidator<CreateCommitCommand>();
        _commitRestClient = Substitute.For<ICommitRestClient>();
        _giteaOptions = Substitute.For<IOptions<GiteaApiConfiguration>>();
        _sut = new CreateCommitCommandHandler(_validator, _commitRestClient, _giteaOptions);
    }

    [Fact]
    public async Task Handle_ShouldThrowsValidationException_WhenInputIsInvalid()
    {
        // Arrange
        _validator.RuleFor(x => x).Must(x => false);
        var request = new CreateCommitCommand
        {
            RepositoryName = "fakeRepo",
            BranchName = "fakeBranch",
            CommitMessage = "fakeCommit",
            FileCommitCommands = []
        };

        // Act
        var actual = async () => await _sut.Handle(request, default);

        // Assert
        await actual.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task Handle_ShouldCallCreateCommitAsync_WhenInputIsValid()
    {
        // Arrange
        const string repositoryName = "fakeRepo";
        const string branchName = "fakeBranch";
        const string commitMessage = "fakeCommit";
        const string path = "fakePath";
        const string content = "fakeContent";
        const string repoOwner = "fakeOwner";
        var request = new CreateCommitCommand
        {
            RepositoryName = "fakeRepo",
            BranchName = "fakeBranch",
            CommitMessage = "fakeCommit",
            FileCommitCommands = new List<FileCommitCommandModel>()
            {
                new()
                {
                    Path = path,
                    Content = content,
                    CommitActionCommand = CommitActionCommand.Create
                }
            }
        };
        var giteaConfigurationApi = new GiteaApiConfiguration
        {
            BaseUrl = "fakeUrl",
            PersonalAccessToken = "fakeToken",
            RepositoriesOwner = repoOwner
        };
        _giteaOptions.Value.Returns(giteaConfigurationApi);

        // Act
        await _sut.Handle(request, default);

        // Assert
        await _commitRestClient.Received(1).CreateCommitAsync(repoOwner, repositoryName, Arg.Is<CreateCommitRequest>(
            x => x.BranchName == branchName &&
                 x.CommitMessage == commitMessage &&
                 x.FileCommitRequests[0].Content == content &&
                 x.FileCommitRequests[0].Path == path &&
                 x.FileCommitRequests[0].CommitAction == CommitAction.Create
        ));
    }
}