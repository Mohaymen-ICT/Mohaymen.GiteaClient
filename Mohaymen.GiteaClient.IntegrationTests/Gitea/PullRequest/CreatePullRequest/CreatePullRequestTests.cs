using System.Net;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.Gitea.Client.Abstractions;
using Mohaymen.GiteaClient.Gitea.PullRequest.CreatePullRequest.Dtos;
using Mohaymen.GiteaClient.IntegrationTests.Common.Collections.Gitea;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData.Abstractions;
using Mohaymen.GiteaClient.IntegrationTests.Common.Models;

namespace Mohaymen.GiteaClient.IntegrationTests.Gitea.PullRequest.CreatePullRequest;

[Collection("GiteaIntegrationTests")]
public class CreatePullRequestTests
{
    private readonly IGiteaClient _sut;
    private readonly ITestRepositoryCreator _repositoryCreator;
    private readonly ITestBranchCreator _branchCreator;
    private readonly GiteaCollectionFixture _giteaCollectionFixture;
    
    public CreatePullRequestTests(GiteaCollectionFixture giteaCollectionFixture)
    {
        _giteaCollectionFixture = giteaCollectionFixture ?? throw new ArgumentNullException(nameof(giteaCollectionFixture));
        _repositoryCreator = _giteaCollectionFixture.ServiceProvider.GetRequiredService<ITestRepositoryCreator>();
        _branchCreator = _giteaCollectionFixture.ServiceProvider.GetRequiredService<ITestBranchCreator>();
        _sut = giteaCollectionFixture.ServiceProvider.GetRequiredService<IGiteaClient>();
    }

    [Fact]
    public async Task CreatePullRequest_ShouldGetBranchListOfRepo_WhenInputsAreProvidedProperly()
    {
        // Arrange
        const string repositoryName = GiteaTestConstants.RepositoryName;
        const string branchName = "test_branch";
        var cancellationToken = _giteaCollectionFixture.CancellationToken;

        await _repositoryCreator.CreateRepositoryAsync(repositoryName, cancellationToken);
        await _branchCreator.CreateBranchAsync(repositoryName, branchName, cancellationToken);
        
        const string title = "title";
        var createPullRequestCommandDto = new CreatePullRequestCommandDto
        {
            RepositoryName = repositoryName,
            Title = title,
            HeadBranch = branchName,
            BaseBranch = GiteaTestConstants.DefaultBranch
        };

        // Act
        var actual = await _sut.PullRequestClient.CreatePullRequestAsync(createPullRequestCommandDto, cancellationToken);

        // Assert
        actual.StatusCode.Should().Be(HttpStatusCode.Created);
        actual.Content!.Title.Should().Be(title);
    }
}