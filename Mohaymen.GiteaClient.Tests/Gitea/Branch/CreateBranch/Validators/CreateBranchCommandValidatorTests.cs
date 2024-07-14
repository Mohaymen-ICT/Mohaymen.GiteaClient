using FluentAssertions;
using FluentValidation;
using Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Commands;
using Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Validators;
using Xunit;

namespace Mohaymen.GiteaClient.Tests.Gitea.Branch.CreateBranch.Validators;

public class CreateBranchCommandValidatorTests
{
    private readonly IValidator<CreateBranchCommand> _sut;

    public CreateBranchCommandValidatorTests()
    {
        _sut = new CreateBranchCommandValidator();
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Validate_ShouldReturnEmptyRepositoryNameErrorCode_WhenRepositoryNameIsNullOrEmpty(string repositoryName)
    {
        // Arrange
        var command = new CreateBranchCommand
        {
            RepositoryName = repositoryName
        };

        // Act
        var actual = _sut.Validate(command);

        // Assert
        actual.Errors.Select(x => x.ErrorCode)
            .Should().Contain(CreateBranchErrorCodes.EmptyRepositoryNameErrorCode);
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Validate_ShouldReturnEmptyNewBranchNameErrorCode_WhenNewBranchNameIsNullOrEmpty(string newBranchName)
    {
        // Arrange
        var command = new CreateBranchCommand
        {
            NewBranchName = newBranchName
        };

        // Act
        var actual = _sut.Validate(command);

        // Assert
        actual.Errors.Select(x => x.ErrorCode)
            .Should().Contain(CreateBranchErrorCodes.EmptyNewBranchNameErrorCode);
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Validate_ShouldReturnEmptyOldReferenceNameErrorCode_WhenOldReferenceNameIsNullOrEmpty(string oldReferenceName)
    {
        // Arrange
        var command = new CreateBranchCommand
        {
            OldReferenceName = oldReferenceName
        };

        // Act
        var actual = _sut.Validate(command);

        // Assert
        actual.Errors.Select(x => x.ErrorCode)
            .Should().Contain(CreateBranchErrorCodes.EmptyOldReferenceNameErrorCode);
    }
    
    [Fact]
    public void Validate_ShouldReturnValidResult_WhenEveryThingIsProvidedProperly()
    {
        // Arrange
        var command = new CreateBranchCommand
        {
            RepositoryName = "repo",
            NewBranchName = "new_branch",
            OldReferenceName = "old_ref"
        };

        // Act
        var actual = _sut.Validate(command);
        
        // Assert
        actual.IsValid.Should().BeTrue();
    }
}