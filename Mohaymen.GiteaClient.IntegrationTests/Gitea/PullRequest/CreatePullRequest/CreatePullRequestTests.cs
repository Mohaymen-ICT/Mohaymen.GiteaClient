using System.Net;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.Gitea.Client.Abstractions;
using Mohaymen.GiteaClient.Gitea.PullRequest.CreatePullRequest.Dtos;
using Mohaymen.GiteaClient.IntegrationTests.Common.Collections.Gitea;
using Mohaymen.GiteaClient.IntegrationTests.Common.Models;

namespace Mohaymen.GiteaClient.IntegrationTests.Gitea.PullRequest.CreatePullRequest;

[Collection("GiteaIntegrationTests")]
public class CreatePullRequestTests : IClassFixture<CreatePullRequestTestsClassFixture>
{
    private readonly IGiteaClient _sut;
    private readonly GiteaCollectionFixture _giteaCollectionFixture;
    
    public CreatePullRequestTests(GiteaCollectionFixture giteaCollectionFixture)
    {
        _giteaCollectionFixture = giteaCollectionFixture ?? throw new ArgumentNullException(nameof(giteaCollectionFixture));
        _sut = giteaCollectionFixture.ServiceProvider.GetRequiredService<IGiteaClient>();
    }

    [Fact]
    public async Task CreatePullRequest_ShouldGetBranchListOfRepo_WhenInputsAreProvidedProperly()
    {
        // Arrange
        const string title = "title";
        var createPullRequestCommandDto = new CreatePullRequestCommandDto
        {
            RepositoryName = GiteaTestConstants.RepositoryName,
            Title = title,
            HeadBranch = CreatePullRequestTestsClassFixture.BranchName,
            BaseBranch = GiteaTestConstants.DefaultBranch
        };

        // Act
        var actual = await _sut.PullRequestClient.CreatePullRequestAsync(createPullRequestCommandDto, _giteaCollectionFixture.CancellationToken);

        // Assert
        actual.StatusCode.Should().Be(HttpStatusCode.Created);
        actual.Content!.Title.Should().Be(title);
    }
}