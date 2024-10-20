using FluentAssertions;
using FluentValidation;
using Mohaymen.GiteaClient.Gitea.PullRequest.Common.Enums;
using Mohaymen.GiteaClient.Gitea.PullRequest.CreatePullRequest.Validators;
using Mohaymen.GiteaClient.Gitea.PullRequest.MergePullRequest.Commands;
using Mohaymen.GiteaClient.Gitea.PullRequest.MergePullRequest.Validators;
using Xunit;

namespace Mohaymen.GiteaClient.Tests.Gitea.PullRequest.MergePullRequest.Validators;

public class MergePullRequestCommandValidatorTests
{
    private readonly IValidator<MergePullRequestCommand> _sut;

    public MergePullRequestCommandValidatorTests()
    {
        _sut = new MergePullRequestCommandValidator();
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Validate_ShouldReturnEmptyRepositoryNameErrorCode_WhenRepositoryNameIsNullOrEmpty(string repositoryName)
    {
        // Arrange
        var command = new MergePullRequestCommand
        {
            RepositoryName = repositoryName,
            Index = 1,
            MergeType = MergeType.Merge
        };

        // Act
        var actual = _sut.Validate(command);

        // Assert
        actual.Errors.Select(x => x.ErrorCode)
            .Should().Contain(MergePullRequestErrorCodes.EmptyRepositoryNameErrorCode);
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    public void Validate_ShouldReturnIndexLessThanOneErrorCode_WhenIndexIsLessThanOne(int index)
    {
        // Arrange
        var command = new MergePullRequestCommand
        {
            RepositoryName = "repo",
            Index = index,
            MergeType = MergeType.Merge
        };

        // Act
        var actual = _sut.Validate(command);

        // Assert
        actual.Errors.Select(x => x.ErrorCode)
            .Should().Contain(MergePullRequestErrorCodes.IndexLessThanOneErrorCode);
    }
    
    [Fact]
    public void Validate_ShouldReturnValidResult_WhenEveryThingIsProvidedProperly()
    {
        // Arrange
        var command = new MergePullRequestCommand
        {
            RepositoryName = "repo",
            Index = 1,
            MergeType = MergeType.Merge
        };

        // Act
        var actual = _sut.Validate(command);
        
        // Assert
        actual.IsValid.Should().BeTrue();
    }
}