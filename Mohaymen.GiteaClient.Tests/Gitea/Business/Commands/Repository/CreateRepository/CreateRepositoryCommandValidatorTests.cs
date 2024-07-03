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
    public void Validate_ShouldReturnEmptyRepositoryName_WhenRepositoryNameIsNullOrEmpty(string repositoryName)
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
        actual.Errors.Select(x => x.ErrorCode).Should().Contain(ValidationErrorCodes.EmptyRepositoryName);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Validate_ShouldReturnEmptyBranchName_WhenDefaultBranchNameIsNullOrEmpty(string branchName)
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
        actual.Errors.Select(x => x.ErrorCode).Should().Contain(ValidationErrorCodes.EmptyBranchName);
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