namespace Mohaymen.GiteaClient.Tests.Gitea.Commit.GetSingleCommit.Validators;

using FluentAssertions;
using FluentValidation;
using Mohaymen.GiteaClient.Core.Validation;
using Mohaymen.GiteaClient.Gitea.Commit.GetBranchCommits.Queries;
using Mohaymen.GiteaClient.Gitea.Commit.GetBranchCommits.Validators;
using Xunit;

public class GetSingleCommitQueryValidatorTests
{
    private readonly IValidator<GetSingleCommitQuery> _sut;

    public GetSingleCommitQueryValidatorTests()
    {
        _sut = new GetSingleCommitQueryValidator();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Validate_ShouldReturnEmptyRepositoryNameErrorCode_WhenRepositoryNameIsNullOrEmpty(string repositoryName)
    {
        // Arrange
        var query = new GetSingleCommitQuery() { RepositoryName = repositoryName, CommitSha = "sha" };

        // Act
        var actual = _sut.Validate(query);

        // Assert
        actual.Errors.Select(x => x.ErrorCode).Should().Contain(ValidationErrorCodes.EmptyRepositoryNameErrorCode);
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Validate_ShouldReturnEmptyCommitShaErrorCode_WhenCommitShaIsNullOrEmpty(string sha)
    {
        // Arrange
        var command = new GetSingleCommitQuery()
        {
            RepositoryName = "testRepo",
            CommitSha = sha
        };

        // Act
        var actual = _sut.Validate(command);

        // Assert
        actual.Errors.Select(x => x.ErrorCode).Should().Contain(ValidationErrorCodes.EmptyCommitShaErrorCode);
    }
    
}