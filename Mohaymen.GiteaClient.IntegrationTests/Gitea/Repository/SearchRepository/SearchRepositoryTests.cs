using System.Net;
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.Gitea.Client.Abstractions;
using Mohaymen.GiteaClient.Gitea.Repository.SearchRepository.Dtos;
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

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task SearchRepositoryAsync_ShouldThrowsValidationException_WhenSearchQueryIsNullOrEmpty(string query)
    {
        // Arrange
        var searchRepositoryQuery = new SearchRepositoryQueryDto
        {
            Query = query
        };
        
        // Act
        var actual = async () => await _sut.RepositoryClient.SearchRepositoryAsync(searchRepositoryQuery, _giteaCollectionFixture.CancellationToken);

        // Assert
        await actual.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task SearchRepositoryAsync_ShouldReturnListOfMatchedRepositories_WhenSearchQueryIsProvidedAndHasMathc()
    {
        // Arrange
        var searchRepositoryQuery = new SearchRepositoryQueryDto
        {
            Query = "Repo"
        };
        
        // Act
        var actual =  await _sut.RepositoryClient.SearchRepositoryAsync(searchRepositoryQuery, _giteaCollectionFixture.CancellationToken);

        // Assert
        actual.StatusCode.Should().Be(HttpStatusCode.OK);
        actual.Content!.SearchResult.Select(x => x.RepositoryName).Should()
            .Contain(RepositoryClassFixture.RepositoryName);
    }
    
    
}