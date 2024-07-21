using FluentAssertions;
using FluentValidation;
using Mohaymen.GiteaClient.Core.Validation;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Commands;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Validators;
using Xunit;

namespace Mohaymen.GiteaClient.Tests.Gitea.Commit.CreateCommit.Validators;

public class FileCommitCommandModelValidatorTests
{
    private readonly IValidator<FileCommitCommandModel> _sut;

    public FileCommitCommandModelValidatorTests()
    {
        _sut = new FileCommitCommandModelValidator();
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validate_ShouldReturnInvalidFilePathErrorCode_WhenFilePathIsNullOrEmpty(string filePath)
    {
        // Arrange
        var fileCommitDto = new FileCommitCommandModel
        {
            Path = filePath,
            Content = "fakeContent",
            CommitActionCommand = CommitActionCommand.Create
        };

        // Act
        var actual = _sut.Validate(fileCommitDto);

        // Assert
        actual.Errors.Select(x => x.ErrorCode).Should().Contain(ValidationErrorCodes.InvalidFilePathErrorCode);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validate_ShouldReturnInvalidFileContentErrorCode_WhenFileContentIsNullOrEmpty(string fileContent)
    {
        // Arrange
        var fileCommitDto = new FileCommitCommandModel
        {
            Path = "fakePath",
            Content = fileContent,
            CommitActionCommand = CommitActionCommand.Create
        };

        // Act
        var actual = _sut.Validate(fileCommitDto);

        // Assert
        actual.Errors.Select(x => x.ErrorCode).Should().Contain(ValidationErrorCodes.InvalidFileContentErrorCode);
    }

    [Fact]
    public void Validate_ShouldReturnValidResult_WhenInputIsProvidedProperly()
    {
        // Arrange
        var fileCommitDto = new FileCommitCommandModel
        {
            Path = "fakePath",
            Content = "fakeContent",
            CommitActionCommand = CommitActionCommand.Create
        };
        
        // Act
        var actual = _sut.Validate(fileCommitDto);
        
        // Assert
        actual.IsValid.Should().BeTrue();
    }
    
}