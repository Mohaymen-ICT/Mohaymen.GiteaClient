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
            RepositoryName = repositoryName
        };

        // Act
        var actual = _sut.Validate(command);

        // Assert
        actual.Errors.Select(x => x.ErrorCode)
            .Should().Contain(CreatePullRequestErrorCodes.EmptyRepositoryNameErrorCode);
    }
    
    [Fact]
    public void Validate_ShouldReturnValidResult_WhenEveryThingIsProvidedProperly()
    {
        // Arrange
        var command = new CreatePullRequestCommand
        {
            RepositoryName = "repo"
        };

        // Act
        var actual = _sut.Validate(command);
        
        // Assert
        actual.IsValid.Should().BeTrue();
    }
}