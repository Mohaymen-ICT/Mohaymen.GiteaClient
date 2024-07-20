using FluentAssertions;
using FluentValidation;
using Mohaymen.GiteaClient.Core.Validation;
using Mohaymen.GiteaClient.Gitea.Repository.SearchRepository.Queries;
using Mohaymen.GiteaClient.Gitea.Repository.SearchRepository.Validators;
using Xunit;

namespace Mohaymen.GiteaClient.Tests.Gitea.Repository.SearchRepository.Validators;

public class SearchRepositoryQueryValidatorTests
{
    private readonly IValidator<SearchRepositoryQuery> _sut;

    public SearchRepositoryQueryValidatorTests()
    {
        _sut = new SearchRepositoryQueryValidator();
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validate_ShouldReturnEmptySearchQueryErrorCode_WhenQueryIsNullOrEmpty(string query)
    {
        // Arrange
        var searchRepositoryQuery = new SearchRepositoryQuery
        {
            Query = query
        };

        // Act
        var actual = _sut.Validate(searchRepositoryQuery);

        // Assert
        actual.Errors.Select(x => x.ErrorCode).Should().Contain(ValidationErrorCodes.EmptySearchQueryErrorCode);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validate_ShouldReturnInvalidPageSizeErrorCode_WhenPageSizeIsLowerThanOne(int pageSize)
    {
        // Arrange
        var searchRepositoryQuery = new SearchRepositoryQuery
        {
            Query = "query",
            Page = pageSize
        };

        // Act
        var actual = _sut.Validate(searchRepositoryQuery);

        // Assert
        actual.Errors.Select(x => x.ErrorCode).Should().Contain(ValidationErrorCodes.InvalidPageSizeErrorCode);
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validate_ShouldReturnInvalidPageSizeErrorCode_WhenLimitIsLowerThanZero(int limit)
    {
        // Arrange
        var searchRepositoryQuery = new SearchRepositoryQuery
        {
            Query = "query",
            Page = 1,
            Limit = limit
        };

        // Act
        var actual = _sut.Validate(searchRepositoryQuery);

        // Assert
        actual.Errors.Select(x => x.ErrorCode).Should().Contain(ValidationErrorCodes.InvalidLimitErrorCode);
    }

    [Fact]
    public void Validate_ShouldReturnValidResult_WhenInputIsProvidedProperly()
    {
        // Arrange
        var searchRepositoryQuery = new SearchRepositoryQuery
        {
            Query = "test"
        };
        
        // Act
        var actual = _sut.Validate(searchRepositoryQuery);

        // Assert
        actual.IsValid.Should().BeTrue();
    }
    
}