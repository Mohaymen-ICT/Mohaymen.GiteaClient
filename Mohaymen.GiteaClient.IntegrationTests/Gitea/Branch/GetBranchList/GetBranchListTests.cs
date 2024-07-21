using System.Net;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.Gitea.Branch.GetBranchList.Dtos;
using Mohaymen.GiteaClient.Gitea.Client.Abstractions;
using Mohaymen.GiteaClient.IntegrationTests.Common.Collections.Gitea;
using Mohaymen.GiteaClient.IntegrationTests.Common.Models;

namespace Mohaymen.GiteaClient.IntegrationTests.Gitea.Branch.GetBranchList;

[Collection("GiteaIntegrationTests")]
public class GetBranchListTests : IClassFixture<GetBranchListTestsClassFixture>
{
    private readonly IGiteaClient _sut;
    private readonly GiteaCollectionFixture _giteaCollectionFixture;
    
    public GetBranchListTests(GiteaCollectionFixture giteaCollectionFixture)
    {
        _giteaCollectionFixture = giteaCollectionFixture ?? throw new ArgumentNullException(nameof(giteaCollectionFixture));
        _sut = giteaCollectionFixture.ServiceProvider.GetRequiredService<IGiteaClient>();
    }
    
    [Fact]
    public async Task GetBranchList_ShouldGetBranchListOfRepo_WhenInputsAreProvidedProperly()
    {
        // Arrange
        var getBranchListCommandDto = new GetBranchListCommandDto
        {
            RepositoryName = GiteaTestConstants.RepositoryName
        };
        var expectedBranchNames = new List<string>
        {
            GiteaTestConstants.DefaultBranch,
            GetBranchListTestsClassFixture.FirstBranch,
            GetBranchListTestsClassFixture.SecondBranch,
            GetBranchListTestsClassFixture.ThirdBranch,
        };

        // Act
        var actual = await _sut.BranchClient.GetBranchListAsync(getBranchListCommandDto, _giteaCollectionFixture.CancellationToken);

        // Assert
        actual.StatusCode.Should().Be(HttpStatusCode.OK);
        actual.Content!.Select(x => x.BranchName).Should().Contain(expectedBranchNames);
    }
}