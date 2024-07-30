using FluentAssertions;
using FluentValidation;
using Mohaymen.GiteaClient.Gitea.PullRequest.CreatePullRequest.Commands;
using Mohaymen.GiteaClient.Gitea.PullRequest.CreatePullRequest.Validators;
using Xunit;

namespace Mohaymen.GiteaClient.Tests.Gitea.PullRequest.CreatePullRequest.Validators;

public class CreatePullRequestCommandValidatorTests
{
    private readonly IValidator<CreatePullRequestCommand> _sut;

    public CreatePullRequestCommandValidatorTests()
    {
        _sut = new CreatePullRequestCommandValidator();
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Validate_ShouldReturnEmptyRepositoryNameErrorCode_WhenRepositoryNameIsNullOrEmpty(string repositoryName)
    {
        // Arrange
        var command = new CreatePullRequestCommand
        {
            RepositoryName = repositoryName,
            Title = "title",
            HeadBranch = "head",
            BaseBranch = "base"
        };

        // Act
        var actual = _sut.Validate(command);

        // Assert
        actual.Errors.Select(x => x.ErrorCode)
            .Should().Contain(CreatePullRequestErrorCodes.EmptyRepositoryNameErrorCode);
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Validate_ShouldReturnEmptyTitleErrorCode_WhenRepositoryNameIsNullOrEmpty(string title)
    {
        // Arrange
        var command = new CreatePullRequestCommand
        {
            RepositoryName = "repo",
            Title = title,
            HeadBranch = "head",
            BaseBranch = "base"
        };

        // Act
        var actual = _sut.Validate(command);

        // Assert
        actual.Errors.Select(x => x.ErrorCode)
            .Should().Contain(CreatePullRequestErrorCodes.EmptyTitleErrorCode);
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Validate_ShouldReturnEmptyHeadBranchErrorCode_WhenRepositoryNameIsNullOrEmpty(string headBranch)
    {
        // Arrange
        var command = new CreatePullRequestCommand
        {
            RepositoryName = "repo",
            Title = "title",
            HeadBranch = headBranch,
            BaseBranch = "base"
        };

        // Act
        var actual = _sut.Validate(command);

        // Assert
        actual.Errors.Select(x => x.ErrorCode)
            .Should().Contain(CreatePullRequestErrorCodes.EmptyHeadBranchErrorCode);
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Validate_ShouldReturnEmptyBaseBranchErrorCode_WhenRepositoryNameIsNullOrEmpty(string baseBranch)
    {
        // Arrange
        var command = new CreatePullRequestCommand
        {
            RepositoryName = "repo",
            Title = "title",
            HeadBranch = "head",
            BaseBranch = baseBranch
        };

        // Act
        var actual = _sut.Validate(command);

        // Assert
        actual.Errors.Select(x => x.ErrorCode)
            .Should().Contain(CreatePullRequestErrorCodes.EmptyBaseBranchErrorCode);
    }
    
    [Fact]
    public void Validate_ShouldReturnValidResult_WhenEveryThingIsProvidedProperly()
    {
        // Arrange
        var command = new CreatePullRequestCommand
        {
            RepositoryName = "repo",
            Title = "title",
            HeadBranch = "head",
            BaseBranch = "base"
        };

        // Act
        var actual = _sut.Validate(command);
        
        // Assert
        actual.IsValid.Should().BeTrue();
    }
}