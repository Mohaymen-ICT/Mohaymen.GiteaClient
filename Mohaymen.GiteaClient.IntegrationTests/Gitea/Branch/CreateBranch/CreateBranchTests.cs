using System.Net;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Dtos;
using Mohaymen.GiteaClient.Gitea.Client.Abstractions;
using Mohaymen.GiteaClient.IntegrationTests.Common.Assertions.Abstractions;
using Mohaymen.GiteaClient.IntegrationTests.Common.Collections.Gitea;
using Mohaymen.GiteaClient.IntegrationTests.Common.Models;

namespace Mohaymen.GiteaClient.IntegrationTests.Gitea.Branch.CreateBranch;

[Collection("GiteaIntegrationTests")]
public class CreateBranchTests : IClassFixture<CreateBranchTestsClassFixture>
{
    private readonly IGiteaClient _sut;
    private readonly ITestBranchChecker _branchChecker;
    private readonly GiteaCollectionFixture _giteaCollectionFixture;
    
    public CreateBranchTests(GiteaCollectionFixture giteaCollectionFixture)
    {
        _giteaCollectionFixture = giteaCollectionFixture ?? throw new ArgumentNullException(nameof(giteaCollectionFixture));
        _sut = giteaCollectionFixture.ServiceProvider.GetRequiredService<IGiteaClient>();
        _branchChecker = _giteaCollectionFixture.ServiceProvider.GetRequiredService<ITestBranchChecker>();
    }
    
    [Fact]
    public async Task CreateBranch_ShouldCreateBranchWithCreatedStatusCode_WhenInputsAreProvidedProperly()
    {
        // Arrange
        const string newBranchName = "feature/test_new_branch";
        var createBranchCommandDto = new CreateBranchCommandDto
        {
            RepositoryName = GiteaTestConstants.RepositoryName,
            NewBranchName = newBranchName,
            OldReferenceName = "main"
        };

        // Act
        var actual = await _sut.BranchClient.CreateBranchAsync(createBranchCommandDto, _giteaCollectionFixture.CancellationToken);

        // Assert
        actual.StatusCode.Should().Be(HttpStatusCode.Created);
        actual.Content!.BranchName.Should().Be(newBranchName);
        var branchExist = await _branchChecker.ContainsBranch(GiteaTestConstants.RepositoryName, newBranchName, _giteaCollectionFixture.CancellationToken);
        branchExist.Should().BeTrue();
    }
}