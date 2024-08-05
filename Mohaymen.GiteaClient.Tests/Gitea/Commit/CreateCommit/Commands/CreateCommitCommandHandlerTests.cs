using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using Mohaymen.GiteaClient.Core.Configs;
using Mohaymen.GiteaClient.Gitea.Commit.Common.ApiCall;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Commands;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Context;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Dtos.Response;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Services.Base64Encoder.Abstractions;
using NSubstitute;
using Refit;
using Xunit;

namespace Mohaymen.GiteaClient.Tests.Gitea.Commit.CreateCommit.Commands;

public class CreateCommitCommandHandlerTests
{
    private readonly InlineValidator<CreateCommitCommand> _validator;
    private readonly ICommitRestClient _commitRestClient;
    private readonly IOptions<GiteaApiConfiguration> _giteaOptions;
    private readonly IBase64CommitEncoder _base64CommitEncoder;
    private readonly IRequestHandler<CreateCommitCommand, ApiResponse<CreateCommitResponseDto>> _sut;

    public CreateCommitCommandHandlerTests()
    {
        _validator = new InlineValidator<CreateCommitCommand>();
        _commitRestClient = Substitute.For<ICommitRestClient>();
        _giteaOptions = Substitute.For<IOptions<GiteaApiConfiguration>>();
        _base64CommitEncoder = Substitute.For<IBase64CommitEncoder>();
        _sut = new CreateCommitCommandHandler(_validator, _commitRestClient, _giteaOptions, _base64CommitEncoder);
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
        const string fileSha = "fakeSha";
        var fileCommitCommandModel = new FileCommitCommandModel
        {
            Path = path,
            Content = content,
            CommitActionCommand = CommitActionCommand.Create,
            FileSha = fileSha
        };
        
        var request = new CreateCommitCommand
        {
            RepositoryName = "fakeRepo",
            BranchName = "fakeBranch",
            CommitMessage = "fakeCommit",
            FileCommitCommands =
            [
                fileCommitCommandModel
            ]
        };
        var giteaConfigurationApi = new GiteaApiConfiguration
        {
            BaseUrl = "fakeUrl",
            PersonalAccessToken = "fakeToken",
            RepositoriesOwner = repoOwner
        };
        _giteaOptions.Value.Returns(giteaConfigurationApi);
        _base64CommitEncoder.EncodeFileContentsToBase64(Arg.Is<IReadOnlyList<FileCommitCommandModel>>(x => 
            x.Count == 1 &&
            x[0].Equals(fileCommitCommandModel)))
            .Returns(new FileCommitCommandModel[]
            {
                fileCommitCommandModel
            });
        var expectedFileCommitRequestArg = new FileCommitRequest
        {
            Path = path,
            Content = content,
            CommitAction = CommitAction.Create,
            FileSha = fileSha
        };
        
        // Act
        await _sut.Handle(request, default);

        // Assert
        await _commitRestClient.Received(1).CreateCommitAsync(repoOwner, repositoryName, Arg.Is<CreateCommitRequest>(
            x => x.BranchName == branchName &&
                 x.CommitMessage == commitMessage &&
                 x.FileCommitRequests[0].Equals(expectedFileCommitRequestArg)
        ));
    }
}