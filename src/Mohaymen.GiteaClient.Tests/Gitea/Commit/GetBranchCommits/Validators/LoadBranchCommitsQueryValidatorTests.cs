using FluentAssertions;
using FluentValidation;
using Mohaymen.GiteaClient.Core.Validation;
using Mohaymen.GiteaClient.Gitea.Commit.GetBranchCommits.Queries;
using Mohaymen.GiteaClient.Gitea.Commit.GetBranchCommits.Validators;
using Xunit;

namespace Mohaymen.GiteaClient.Tests.Gitea.Commit.GetBranchCommits.Validators;

public class LoadBranchCommitsQueryValidatorTests
{
    private readonly IValidator<LoadBranchCommitsQuery> _sut;

    public LoadBranchCommitsQueryValidatorTests()
    {
        _sut = new LoadBranchCommitsQueryValidator();    
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Validate_ShouldReturnEmptyRepositoryNameErrorCode_WhenRepositoryNameIsNullOrEmpty(string repositoryName)
    {
        // Arrange
        var command = new LoadBranchCommitsQuery
        {
            RepositoryName = repositoryName,
            BranchName = "main",
            Limit = 10,
            Page = 1
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
        var command = new LoadBranchCommitsQuery
        {
            RepositoryName = "testRepo",
            BranchName = branchName,
            Limit = 10,
            Page = 1
        };

        // Act
        var actual = _sut.Validate(command);

        // Assert
        actual.Errors.Select(x => x.ErrorCode).Should().Contain(ValidationErrorCodes.EmptyBranchNameErrorCode);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-5)]
    public void Validate_ShouldReturnInvalidPageSizeErrorCode_WhenPageSizeIsLowerThanOne(int page)
    {
        // Arrange
        var command = new LoadBranchCommitsQuery
        {
            RepositoryName = "testRepo",
            BranchName = "fakeBranch",
            Limit = 10,
            Page = page
        };
        
        // Act
        var actual = _sut.Validate(command);
        
        // Assert
        actual.Errors.Select(x => x.ErrorCode).Should().Contain(ValidationErrorCodes.InvalidPageSizeErrorCode);
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-5)]
    public void Validate_ShouldReturnInvalidLimitErrorCode_WhenLimitIsLowerThanOne(int limit)
    {
        // Arrange
        var command = new LoadBranchCommitsQuery
        {
            RepositoryName = "testRepo",
            BranchName = "fakeBranch",
            Limit = limit,
            Page = 1
        };
        
        // Act
        var actual = _sut.Validate(command);
        
        // Assert
        actual.Errors.Select(x => x.ErrorCode).Should().Contain(ValidationErrorCodes.InvalidLimitErrorCode);
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(1, 10)]
    [InlineData(10, 1)]
    [InlineData(2, 2)]
    public void Validate_ShouldReturnValidResult_WhenInputIsProvidedProperly(int page, int limit)
    {
        // Arrange
        var command = new LoadBranchCommitsQuery
        {
            RepositoryName = "testRepo",
            BranchName = "fakeBranch",
            Limit = limit,
            Page = page
        };
        
        // Act
        var actual = _sut.Validate(command);
        
        // Assert
        actual.IsValid.Should().BeTrue();
    }
    
}