using FluentAssertions;
using FluentValidation;
using Mohaymen.GiteaClient.Core.Validation;
using Mohaymen.GiteaClient.Gitea.File.GetFilesMetadata.Queries;
using Mohaymen.GiteaClient.Gitea.File.GetFilesMetadata.Validators;
using Xunit;

namespace Mohaymen.GiteaClient.Tests.Gitea.File.GetFilesMetadata.Validators;

public class GetFilesMetadataQueryValidatorTests
{
    private readonly IValidator<GetFilesMetadataQuery> _sut;

    public GetFilesMetadataQueryValidatorTests()
    {
        _sut = new GetFilesMetadataQueryValidator();
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validate_ShouldReturnEmptyRepositoryNameErrorCode_WhenRepositoryNameIsNullOrEmpty(string repositoryName)
    {
        // Arrange
        var query = new GetFilesMetadataQuery
        {
            RepositoryName = repositoryName,
            BranchName = "fakeBranchName"
        };

        // Act
        var actual = _sut.Validate(query);

        // Assert
        actual.Errors.Select(x => x.ErrorCode).Should().Contain(ValidationErrorCodes.EmptyRepositoryNameErrorCode);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validate_ShouldReturnEmptyBranchNameErrorCode_WhenBranchNameIsNullOrEmpty(string branchName)
    {
        // Arrange
        var query = new GetFilesMetadataQuery
        {
            RepositoryName = "fakeRepositoryName",
            BranchName = branchName
        };

        // Act
        var actual = _sut.Validate(query);

        // Assert
        actual.Errors.Select(x => x.ErrorCode).Should().Contain(ValidationErrorCodes.EmptyBranchNameErrorCode);
    }

    [Fact]
    public void Validate_ShouldReturnValidResult_WhenInputIsProvidedProperly()
    {
        // Arrange
        var query = new GetFilesMetadataQuery
        {
            RepositoryName = "fakeRepositoryName",
            BranchName = "fakeBranchName"
        };
        
        // Act
        var actual = _sut.Validate(query);
        
        // Assert
        actual.IsValid.Should().BeTrue();
    }
    
}