using FluentAssertions;
using FluentValidation;
using Mohaymen.GiteaClient.Core.Validation;
using Mohaymen.GiteaClient.Gitea.Repository.DeleteRepository.Commands;
using Mohaymen.GiteaClient.Gitea.Repository.DeleteRepository.Validators;
using Xunit;

namespace Mohaymen.GiteaClient.Tests.Gitea.Repository.DeleteRepository.Validators;

public class DeleteRepositoryCommandValidatorTests
{
    private readonly IValidator<DeleteRepositoryCommand> _sut;

    public DeleteRepositoryCommandValidatorTests()
    {
        _sut = new DeleteRepositoryCommandValidator();
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validate_ShouldReturnEmptyRepositoryNameErrorCode_WhenRepositoryNameIsNullOrEmpty(string repositoryName)
    {
        // Arrange
        var deleteRepositoryCommand = new DeleteRepositoryCommand
        {
            RepositoryName = repositoryName
        };

        // Act
        var actual = _sut.Validate(deleteRepositoryCommand);

        // Assert
        actual.Errors.Select(x => x.ErrorCode).Should().Contain(ValidationErrorCodes.EmptyRepositoryNameErrorCode);
    }

    [Fact]
    public void Validate_ShouldReturnValidResult_WhenRepositoryNameIsProvidedProperly()
    {
        // Arrange
        var deleteRepositoryCommand = new DeleteRepositoryCommand
        {
            RepositoryName = "FakeRepositoryName"
        };

        // Act
        var actual = _sut.Validate(deleteRepositoryCommand);

        // Assert
        actual.IsValid.Should().BeTrue();
    }
}