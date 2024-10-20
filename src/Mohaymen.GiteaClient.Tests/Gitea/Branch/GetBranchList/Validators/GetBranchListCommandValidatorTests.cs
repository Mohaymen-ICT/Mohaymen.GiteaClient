using FluentAssertions;
using FluentValidation;
using Mohaymen.GiteaClient.Gitea.Branch.GetBranchList.Commands;
using Mohaymen.GiteaClient.Gitea.Branch.GetBranchList.Validators;
using Xunit;

namespace Mohaymen.GiteaClient.Tests.Gitea.Branch.GetBranchList.Validators;

public class GetBranchListCommandValidatorTests
{
    private readonly IValidator<GetBranchListCommand> _sut;

    public GetBranchListCommandValidatorTests()
    {
        _sut = new GetBranchListCommandValidator();
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Validate_ShouldReturnEmptyRepositoryNameErrorCode_WhenRepositoryNameIsNullOrEmpty(string repositoryName)
    {
        // Arrange
        var command = new GetBranchListCommand
        {
            RepositoryName = repositoryName
        };

        // Act
        var actual = _sut.Validate(command);

        // Assert
        actual.Errors.Select(x => x.ErrorCode)
            .Should().Contain(GetBranchListErrorCodes.EmptyRepositoryNameErrorCode);
    }
    
    [Fact]
    public void Validate_ShouldReturnValidResult_WhenEveryThingIsProvidedProperly()
    {
        // Arrange
        var command = new GetBranchListCommand
        {
            RepositoryName = "repo"
        };

        // Act
        var actual = _sut.Validate(command);
        
        // Assert
        actual.IsValid.Should().BeTrue();
    }
}