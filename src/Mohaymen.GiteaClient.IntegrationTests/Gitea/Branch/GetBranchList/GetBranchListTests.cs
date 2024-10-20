using System.Net;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.Gitea.Branch.GetBranchList.Dtos;
using Mohaymen.GiteaClient.Gitea.Client.Abstractions;
using Mohaymen.GiteaClient.IntegrationTests.Common.Collections.Gitea;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData.Abstractions;
using Mohaymen.GiteaClient.IntegrationTests.Common.Models;

namespace Mohaymen.GiteaClient.IntegrationTests.Gitea.Branch.GetBranchList;

[Collection("GiteaIntegrationTests")]
public class GetBranchListTests
{
    private readonly IGiteaClient _sut;
    private readonly ITestRepositoryCreator _repositoryCreator;
    private readonly ITestBranchCreator _branchCreator;
    private readonly GiteaCollectionFixture _giteaCollectionFixture;
    
    public GetBranchListTests(GiteaCollectionFixture giteaCollectionFixture)
    {
        _giteaCollectionFixture = giteaCollectionFixture ?? throw new ArgumentNullException(nameof(giteaCollectionFixture));
        _repositoryCreator = _giteaCollectionFixture.ServiceProvider.GetRequiredService<ITestRepositoryCreator>();
        _branchCreator = _giteaCollectionFixture.ServiceProvider.GetRequiredService<ITestBranchCreator>();
        _sut = _giteaCollectionFixture.ServiceProvider.GetRequiredService<IGiteaClient>();
    }
    
    [Fact]
    public async Task GetBranchList_ShouldGetBranchListOfRepo_WhenInputsAreProvidedProperly()
    {
        // Arrange
        const string repositoryName = "get_branch_list_repo";
        const string branch1 = "get_branch_list_repo_branch_1";
        const string branch2 = "get_branch_list_repo_branch_2";
        const string branch3 = "get_branch_list_repo_branch_3";
        var cancellationToken = _giteaCollectionFixture.CancellationToken;

        await _repositoryCreator.CreateRepositoryAsync(repositoryName, cancellationToken);
        await _branchCreator.CreateBranchAsync(repositoryName, branch1, cancellationToken);
        await _branchCreator.CreateBranchAsync(repositoryName, branch2, cancellationToken);
        await _branchCreator.CreateBranchAsync(repositoryName, branch3, cancellationToken);

        var getBranchListCommandDto = new GetBranchListCommandDto
        {
            RepositoryName = repositoryName
        };
        var expectedBranchNames = new List<string>
        {
            GiteaTestConstants.DefaultBranch,
            branch1,
            branch2,
            branch3
        };

        // Act
        var actual = await _sut.BranchClient.GetBranchListAsync(getBranchListCommandDto, cancellationToken);

        // Assert
        actual.StatusCode.Should().Be(HttpStatusCode.OK);
        actual.Content!.Select(x => x.BranchName).Should().BeEquivalentTo(expectedBranchNames);
    }
}