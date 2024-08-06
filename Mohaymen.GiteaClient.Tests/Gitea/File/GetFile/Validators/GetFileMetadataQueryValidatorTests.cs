using FluentAssertions;
using FluentValidation;
using Mohaymen.GiteaClient.Gitea.File.GetFile.Queries;
using Mohaymen.GiteaClient.Gitea.File.GetFile.Validators;
using Xunit;

namespace Mohaymen.GiteaClient.Tests.Gitea.File.GetFile.Validators;

public class GetFileMetadataQueryValidatorTests
{
    private readonly IValidator<GetFileMetadataQuery> _sut;

    public GetFileMetadataQueryValidatorTests()
    {
        _sut = new GetFileMetadataQueryValidator();
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Validate_ShouldReturnEmptyRepositoryNameErrorCode_WhenRepositoryNameIsNullOrEmpty(string repositoryName)
    {
        // Arrange
        var command = new GetFileMetadataQuery
        {
            RepositoryName = repositoryName,
            FilePath = "filePath"
        };

        // Act
        var actual = _sut.Validate(command);

        // Assert
        actual.IsValid.Should().BeFalse();
        actual.Errors.Select(x => x.ErrorCode)
            .Should().Contain(GetFileErrorCodes.EmptyRepositoryNameErrorCode);
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Validate_ShouldReturnEmptyFilePathErrorCode_WhenFilePathIsNullOrEmpty(string filePath)
    {
        // Arrange
        var command = new GetFileMetadataQuery
        {
            RepositoryName = "repo",
            FilePath = filePath
        };

        // Act
        var actual = _sut.Validate(command);

        // Assert
        actual.IsValid.Should().BeFalse();
        actual.Errors.Select(x => x.ErrorCode)
            .Should().Contain(GetFileErrorCodes.EmptyFilePathErrorCode);
    }
    
    [Fact]
    public void Validate_ShouldReturnValidResult_WhenEveryThingIsProvidedProperly()
    {
        // Arrange
        var command = new GetFileMetadataQuery
        {
            RepositoryName = "repo",
            FilePath = "path"
        };

        // Act
        var actual = _sut.Validate(command);
        
        // Assert
        actual.IsValid.Should().BeTrue();
    }
}