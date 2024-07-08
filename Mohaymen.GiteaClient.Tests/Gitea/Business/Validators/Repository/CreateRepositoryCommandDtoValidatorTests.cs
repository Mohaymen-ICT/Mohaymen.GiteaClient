using FluentAssertions;
using FluentValidation;
using Mohaymen.GiteaClient.Common.Validation;
using Mohaymen.GiteaClient.Gitea.Business.Validators.Repository;
using Mohaymen.GiteaClient.Gitea.Domain.Dtos.Repository.CreateRepository;
using Xunit;

namespace Mohaymen.GitClient.Tests.Gitea.Business.Validators.Repository;

public class CreateRepositoryCommandDtoValidatorTests
{
    private readonly IValidator<CreateRepositoryCommandDto> _sut;

    public CreateRepositoryCommandDtoValidatorTests()
    {
        _sut = new CreateRepositoryCommandDtoValidator();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Validate_ShouldReturnEmptyRepositoryNameErrorCode_WhenRepositoryNameIsNullOrEmpty(string repositoryName)
    {
        // Arrange
        var commandDto = new CreateRepositoryCommandDto
        {
            Name = repositoryName,
            DefaultBranch = "main",
            IsPrivateBranch = true
        };

        // Act
        var actual = _sut.Validate(commandDto);

        // Assert
        actual.Errors.Select(x => x.ErrorCode).Should().Contain(ValidationErrorCodes.EmptyRepositoryNameErrorCode);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Validate_ShouldReturnEmptyBranchNameErrorCode_WhenDefaultBranchNameIsNullOrEmpty(string branchName)
    {
        // Arrange
        var commandDto = new CreateRepositoryCommandDto
        {
            Name = "testRepo",
            DefaultBranch = branchName,
            IsPrivateBranch = true
        };

        // Act
        var actual = _sut.Validate(commandDto);

        // Assert
        actual.Errors.Select(x => x.ErrorCode).Should().Contain(ValidationErrorCodes.EmptyBranchNameErrorCode);
    }

    [Fact]
    public void Validate_ShouldReturnValidResult_WhenEveryThingIsProvidedProperly()
    {
        // Arrange
        var commandDto = new CreateRepositoryCommandDto
        {
            Name = "testRepo",
            DefaultBranch = "main",
            IsPrivateBranch = true
        };

        // Act
        var actual = _sut.Validate(commandDto);
        
        // Assert
        actual.IsValid.Should().BeTrue();
    }
}