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