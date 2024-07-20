using System.Net;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.Gitea.Client.Abstractions;
using Mohaymen.GiteaClient.Gitea.Commit.GetBranchCommits.Dtos;
using Mohaymen.GiteaClient.IntegrationTests.Common.Collections.Gitea;

namespace Mohaymen.GiteaClient.IntegrationTests.Gitea.Commit.GetBranchCommits;

[Collection("GiteaIntegrationTests")]
public class GetBranchCommitsTests : IClassFixture<GetBranchCommitsClassFixture>
{
    private readonly IGiteaClient _sut;
    private readonly GiteaCollectionFixture _giteaCollectionFixture;
    
    public GetBranchCommitsTests(GiteaCollectionFixture giteaCollectionFixture)
    {
        _giteaCollectionFixture = giteaCollectionFixture ?? throw new ArgumentNullException(nameof(giteaCollectionFixture));
        _sut = _giteaCollectionFixture.ServiceProvider.GetRequiredService<IGiteaClient>();
    }

    [Fact]
    public async Task GetBranchCommitsAsync_ShouldReturnOkAndBranchCommits_WhenInputsAreProvided()
    {
        // Arrange
        var loadBranchCommitsDto = new LoadBranchCommitsQueryDto
        {
            RepositoryName = GetBranchCommitsClassFixture.RepositoryName,
            BranchName = GetBranchCommitsClassFixture.BranchName,
            Page = 1,
            Limit = 10
        };

        // Act
        var actual = await _sut.CommitClient.LoadBranchCommitsAsync(loadBranchCommitsDto, _giteaCollectionFixture.CancellationToken);

        // Assert
        actual.StatusCode.Should().Be(HttpStatusCode.OK);
        actual.Content.Should().HaveCount(2);
        actual.Content!.Select(x => x.CommitDto.CommitMessage).Should().Contain($"{GetBranchCommitsClassFixture.CommitMessage}\n");
        actual.Content!.Select(x => x.CommitDto.CommitMessage).Should().Contain("Initial commit\n");
    }

}