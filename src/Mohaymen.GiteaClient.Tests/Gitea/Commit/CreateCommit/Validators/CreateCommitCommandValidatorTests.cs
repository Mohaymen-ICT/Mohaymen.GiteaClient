using FluentAssertions;
using FluentValidation;
using Mohaymen.GiteaClient.Core.Validation;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Commands;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Validators;
using Xunit;

namespace Mohaymen.GiteaClient.Tests.Gitea.Commit.CreateCommit.Validators;

public class CreateCommitCommandValidatorTests
{
    private readonly IValidator<CreateCommitCommand> _sut;

    public CreateCommitCommandValidatorTests()
    {
        _sut = new CreateCommitCommandValidator();
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Validate_ShouldReturnEmptyRepositoryNameErrorCode_WhenRepositoryNameIsNullOrEmpty(string repositoryName)
    {
        // Arrange
        var command = new CreateCommitCommand
        {
            RepositoryName = repositoryName,
            BranchName = "fakeBranch",
            CommitMessage = "fakeMessage",
            FileCommitCommands =
            [
                new FileCommitCommandModel
                {
                    Path = "fakePath",
                    Content = "fakeContent",
                    CommitActionCommand = CommitActionCommand.Create
                }
            ]
        };

        // Act
        var actual = _sut.Validate(command);

        // Assert
        actual.Errors.Select(x => x.ErrorCode).Should().Contain(ValidationErrorCodes.EmptyRepositoryNameErrorCode);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Validate_ShouldReturnEmptyBranchNameErrorCode_WhenBranchNameIsNullOrEmpty(string branchName)
    {
        // Arrange
        var command = new CreateCommitCommand
        {
            RepositoryName = "testRepo",
            BranchName = branchName,
            CommitMessage = "fakeMessage",
            FileCommitCommands =
            [
                new FileCommitCommandModel
                {
                    Path = "fakePath",
                    Content = "fakeContent",
                    CommitActionCommand = CommitActionCommand.Create
                }
            ]
        };

        // Act
        var actual = _sut.Validate(command);

        // Assert
        actual.Errors.Select(x => x.ErrorCode).Should().Contain(ValidationErrorCodes.EmptyBranchNameErrorCode);
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Validate_ShouldReturnEmptyCommitMessageErrorCode_WhenCommitMessageIsNullOrEmpty(string commitMessage)
    {
        // Arrange
        var command = new CreateCommitCommand
        {
            RepositoryName = "testRepo",
            BranchName = "fakeBranch",
            CommitMessage = commitMessage,
            FileCommitCommands =
            [
                new FileCommitCommandModel
                {
                    Path = "fakePath",
                    Content = "fakeContent",
                    CommitActionCommand = CommitActionCommand.Create
                }
            ]
        };

        // Act
        var actual = _sut.Validate(command);

        // Assert
        actual.Errors.Select(x => x.ErrorCode).Should().Contain(ValidationErrorCodes.EmptyCommitMessageErrorCode);
    }
    
    [Fact]
    public void Validate_ShouldReturnValidResult_WhenInputsAreProvidedProperly()
    {
        // Arrange
        var command = new CreateCommitCommand
        {
            RepositoryName = "testRepo",
            BranchName = "fakeBranch",
            CommitMessage = "fakeMessage",
            FileCommitCommands =
            [
                new FileCommitCommandModel
                {
                    Path = "fakePath",
                    Content = "fakeContent",
                    CommitActionCommand = CommitActionCommand.Create
                }
            ]
        };
        
        // Act
        var actual = _sut.Validate(command);

        // Assert
        actual.IsValid.Should().BeTrue();
    }
    
}