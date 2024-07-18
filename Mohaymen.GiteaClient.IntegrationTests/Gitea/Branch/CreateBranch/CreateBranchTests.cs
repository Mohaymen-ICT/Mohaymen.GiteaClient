using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Dtos;
using Mohaymen.GiteaClient.Gitea.Client.Abstractions;
using Mohaymen.GiteaClient.IntegrationTests.Common.Collections.Gitea;

namespace Mohaymen.GiteaClient.IntegrationTests.Gitea.Branch.CreateBranch;

[Collection("GiteaIntegrationTests")]
public class CreateBranchTests
{
    private readonly IGiteaClient _sut;
    private readonly GiteaCollectionFixture _giteaCollectionFixture;
    
    public CreateBranchTests(GiteaCollectionFixture giteaCollectionFixture)
    {
        _giteaCollectionFixture = giteaCollectionFixture ?? throw new ArgumentNullException(nameof(giteaCollectionFixture));
        _sut = giteaCollectionFixture.ServiceProvider.GetRequiredService<IGiteaClient>();
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task CreateBranch_ShouldThrowsValidationException_WhenRepositoryNameIsNullOrEmpty(string repositoryName)
    {
        // Arrange
        var createBranchCommandDto = new CreateBranchCommandDto
        {
            RepositoryName = repositoryName,
            NewBranchName = "feature/new_branch",
            OldReferenceName = "main"
        };
        
        // Act
        var actual = async () => await _sut.BranchClient.CreateBranchAsync(createBranchCommandDto, _giteaCollectionFixture.CancellationToken);

        // Assert
        await actual.Should().ThrowAsync<ValidationException>();
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task CreateBranch_ShouldThrowsValidationException_WhenOldBranchNameIsNullOrEmpty(string branchName)
    {
        // Arrange
        var createBranchCommandDto = new CreateBranchCommandDto
        {
            RepositoryName = "test_repo",
            NewBranchName = "feature/new_branch",
            OldReferenceName = branchName
        };
        
        // Act
        var actual = async () => await _sut.BranchClient.CreateBranchAsync(createBranchCommandDto, _giteaCollectionFixture.CancellationToken);

        // Assert
        await actual.Should().ThrowAsync<ValidationException>();
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task CreateBranch_ShouldThrowsValidationException_WhenNewBranchNameIsNullOrEmpty(string branchName)
    {
        // Arrange
        var createBranchCommandDto = new CreateBranchCommandDto
        {
            RepositoryName = "test_repo",
            NewBranchName = branchName,
            OldReferenceName = "main"
        };
        
        // Act
        var actual = async () => await _sut.BranchClient.CreateBranchAsync(createBranchCommandDto, _giteaCollectionFixture.CancellationToken);

        // Assert
        await actual.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task CreateBranch_ShouldCreateBranchWithCreatedStatusCode_WhenInputsAreProvidedProperly()
    {
        // Arrange
        const string newBranchName = "feature/test_new_branch";
        var createBranchCommandDto = new CreateBranchCommandDto
        {
            RepositoryName = "CreateBranchTestRepo",
            NewBranchName = newBranchName,
            OldReferenceName = "main"
        };

        // Act

        // Assert

    }
    
}