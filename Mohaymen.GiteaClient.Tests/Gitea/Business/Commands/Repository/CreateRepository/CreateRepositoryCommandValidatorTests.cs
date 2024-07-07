using FluentAssertions;
using FluentValidation;
using Mohaymen.GitClient.Common.Validation;
using Mohaymen.GitClient.Gitea.Business.Commands.Repository.CreateRepository;
using Xunit;

namespace Mohaymen.GitClient.Tests.Gitea.Business.Commands.Repository.CreateRepository;

public class CreateRepositoryCommandValidatorTests
{
    private readonly IValidator<CreateRepositoryCommand> _sut;

    public CreateRepositoryCommandValidatorTests()
    {
        _sut = new CreateRepositoryCommandValidator();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Validate_ShouldReturnEmptyRepositoryNameErrorCode_WhenRepositoryNameIsNullOrEmpty(string repositoryName)
    {
        // Arrange
        var command = new CreateRepositoryCommand()
        {
            Name = repositoryName,
            DefaultBranch = "main",
            IsPrivateBranch = true
        };

        // Act
        var actual = _sut.Validate(command);

        // Assert
        actual.Errors.Select(x => x.ErrorCode).Should().Contain(ValidationErrorCodes.EmptyRepositoryNameErrorCode);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Validate_ShouldReturnEmptyBranchNameErrorCode_WhenDefaultBranchNameIsNullOrEmpty(string branchName)
    {
        // Arrange
        var command = new CreateRepositoryCommand()
        {
            Name = "testRepo",
            DefaultBranch = branchName,
            IsPrivateBranch = true
        };

        // Act
        var actual = _sut.Validate(command);

        // Assert
        actual.Errors.Select(x => x.ErrorCode).Should().Contain(ValidationErrorCodes.EmptyBranchNameErrorCode);
    }

    [Fact]
    public void Validate_ShouldReturnValidResult_WhenEveryThingIsProvidedProperly()
    {
        // Arrange
        var command = new CreateRepositoryCommand()
        {
            Name = "testRepo",
            DefaultBranch = "main",
            IsPrivateBranch = true
        };

        // Act
        var actual = _sut.Validate(command);
        
        // Assert
        actual.IsValid.Should().BeTrue();
    }
}