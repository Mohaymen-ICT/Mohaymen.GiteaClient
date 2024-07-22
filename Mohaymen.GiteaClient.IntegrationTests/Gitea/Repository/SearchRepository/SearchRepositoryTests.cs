using System.Net;
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.Gitea.Client.Abstractions;
using Mohaymen.GiteaClient.Gitea.Repository.SearchRepository.Dtos;
using Mohaymen.GiteaClient.IntegrationTests.Common.Assertions.Abstractions;
using Mohaymen.GiteaClient.IntegrationTests.Common.Collections.Gitea;

namespace Mohaymen.GiteaClient.IntegrationTests.Gitea.Repository.SearchRepository;

[Collection("GiteaIntegrationTests")]
public class SearchRepositoryTests : IClassFixture<RepositoryClassFixture>
{
    private readonly IGiteaClient _sut;
    private readonly GiteaCollectionFixture _giteaCollectionFixture;

    public SearchRepositoryTests(GiteaCollectionFixture giteaCollectionFixture)
    {
        _giteaCollectionFixture = giteaCollectionFixture ?? throw new ArgumentNullException(nameof(giteaCollectionFixture));
        _sut = giteaCollectionFixture.ServiceProvider.GetRequiredService<IGiteaClient>();

    }
    
    [Fact]
    public async Task SearchRepositoryAsync_ShouldReturnListOfMatchedRepositories_WhenSearchQueryIsProvidedAndHasMathc()
    {
        // Arrange
        var searchRepositoryQuery = new SearchRepositoryQueryDto
        {
            Query = "Repo",
            Limit = 10,
            Page = 1
        };
        
        // Act
        var actual =  await _sut.RepositoryClient.SearchRepositoryAsync(searchRepositoryQuery, _giteaCollectionFixture.CancellationToken);

        // Assert
        actual.StatusCode.Should().Be(HttpStatusCode.OK);
        actual.Content!.SearchResult.Select(x => x.RepositoryName).Should()
            .Contain(RepositoryClassFixture.SearchRepositoryName);
    }

    [Fact]
    public async Task SearchRepositoryAsync_ShouldReturnEmptyList_WhenSearchQueryIsProvidedAndNotMatch()
    {
        // Arrange
        var searchRepositoryQuery = new SearchRepositoryQueryDto
        {
            Query = "ahmad",
            Page = 1,
            Limit = 5
        };
        
        // Act
        var actual =  await _sut.RepositoryClient.SearchRepositoryAsync(searchRepositoryQuery, _giteaCollectionFixture.CancellationToken);

        // Assert
        actual.StatusCode.Should().Be(HttpStatusCode.OK);
        actual.Content!.SearchResult.Should().NotBeNull();
        actual.Content!.SearchResult.Should().BeEmpty();
    }
}