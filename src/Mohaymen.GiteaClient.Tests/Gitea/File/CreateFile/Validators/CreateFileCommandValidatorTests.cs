using FluentAssertions;
using FluentValidation;
using Mohaymen.GiteaClient.Gitea.File.CreateFile.Commands;
using Mohaymen.GiteaClient.Gitea.File.CreateFile.Validators;
using Xunit;

namespace Mohaymen.GiteaClient.Tests.Gitea.File.CreateFile.Validators;

public class CreateFileCommandValidatorTests
{
    private readonly IValidator<CreateFileCommand> _sut;

    public CreateFileCommandValidatorTests()
    {
        _sut = new CreateFileCommandValidator();
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Validate_ShouldReturnEmptyRepositoryNameErrorCode_WhenRepositoryNameIsNullOrEmpty(string repositoryName)
    {
        // Arrange
        var command = new CreateFileCommand
        {
            RepositoryName = repositoryName,
            FilePath = "filePath",
            Content = "content"
        };

        // Act
        var actual = _sut.Validate(command);

        // Assert
        actual.IsValid.Should().BeFalse();
        actual.Errors.Select(x => x.ErrorCode)
            .Should().Contain(CreateFileErrorCodes.EmptyRepositoryNameErrorCode);
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Validate_ShouldReturnEmptyFilePathErrorCode_WhenFilePathIsNullOrEmpty(string filePath)
    {
        // Arrange
        var command = new CreateFileCommand
        {
            RepositoryName = "repo",
            FilePath = filePath,
            Content = "content"
        };

        // Act
        var actual = _sut.Validate(command);

        // Assert
        actual.IsValid.Should().BeFalse();
        actual.Errors.Select(x => x.ErrorCode)
            .Should().Contain(CreateFileErrorCodes.EmptyFilePathErrorCode);
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Validate_ShouldReturnEmptyContentErrorCode_WhenFilePathIsNullOrEmpty(string content)
    {
        // Arrange
        var command = new CreateFileCommand
        {
            RepositoryName = "repo",
            FilePath = "path",
            Content = content
        };

        // Act
        var actual = _sut.Validate(command);

        // Assert
        actual.IsValid.Should().BeFalse();
        actual.Errors.Select(x => x.ErrorCode)
            .Should().Contain(CreateFileErrorCodes.EmptyContentErrorCode);
    }
    
    [Fact]
    public void Validate_ShouldReturnValidResult_WhenEveryThingIsProvidedProperly()
    {
        // Arrange
        var command = new CreateFileCommand
        {
            RepositoryName = "repo",
            FilePath = "path",
            Content = "content"
        };

        // Act
        var actual = _sut.Validate(command);
        
        // Assert
        actual.IsValid.Should().BeTrue();
    }
}