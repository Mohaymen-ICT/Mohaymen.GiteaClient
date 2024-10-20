using System.Net;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Dtos;
using Mohaymen.GiteaClient.Gitea.Client.Abstractions;
using Mohaymen.GiteaClient.IntegrationTests.Common.Assertions.Abstractions;
using Mohaymen.GiteaClient.IntegrationTests.Common.Collections.Gitea;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData.Abstractions;
using Mohaymen.GiteaClient.IntegrationTests.Common.Models;

namespace Mohaymen.GiteaClient.IntegrationTests.Gitea.Branch.CreateBranch;

[Collection("GiteaIntegrationTests")]
public class CreateBranchTests
{
    private readonly IGiteaClient _sut;
    private readonly ITestBranchChecker _branchChecker;
    private readonly ITestRepositoryCreator _repositoryCreator;
    private readonly GiteaCollectionFixture _giteaCollectionFixture;
    
    public CreateBranchTests(GiteaCollectionFixture giteaCollectionFixture)
    {
        _giteaCollectionFixture = giteaCollectionFixture ?? throw new ArgumentNullException(nameof(giteaCollectionFixture));
        _sut = giteaCollectionFixture.ServiceProvider.GetRequiredService<IGiteaClient>();
        _branchChecker = _giteaCollectionFixture.ServiceProvider.GetRequiredService<ITestBranchChecker>();
        _repositoryCreator = _giteaCollectionFixture.ServiceProvider.GetRequiredService<ITestRepositoryCreator>();
    }
    
    [Fact]
    public async Task CreateBranch_ShouldCreateBranchWithCreatedStatusCode_WhenInputsAreProvidedProperly()
    {
        // Arrange
        const string repositoryName = "create_branch_repo";
        await _repositoryCreator.CreateRepositoryAsync(repositoryName, _giteaCollectionFixture.CancellationToken);
        
        const string newBranchName = "create_branch_branch";
        var createBranchCommandDto = new CreateBranchCommandDto
        {
            RepositoryName = repositoryName,
            NewBranchName = newBranchName,
            OldReferenceName = GiteaTestConstants.DefaultBranch
        };

        // Act
        var actual = await _sut.BranchClient.CreateBranchAsync(createBranchCommandDto, _giteaCollectionFixture.CancellationToken);

        // Assert
        actual.StatusCode.Should().Be(HttpStatusCode.Created);
        actual.Content!.BranchName.Should().Be(newBranchName);
        var branchExist = await _branchChecker.ContainsBranch(repositoryName, newBranchName, _giteaCollectionFixture.CancellationToken);
        branchExist.Should().BeTrue();
    }
}