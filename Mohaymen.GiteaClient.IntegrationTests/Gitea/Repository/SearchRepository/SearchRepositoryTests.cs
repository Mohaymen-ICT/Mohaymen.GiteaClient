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
    
    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task SearchRepositoryAsync_ShouldThrowsValidationException_WhenPageSizeIsLowerThan1(int pageSize)
    {
        // Arrange
        var searchRepositoryQuery = new SearchRepositoryQueryDto
        {
            Query = "fakeQuery",
            Page = pageSize,
            Limit = 10
        };
        
        // Act
        var actual = async () => await _sut.RepositoryClient.SearchRepositoryAsync(searchRepositoryQuery, _giteaCollectionFixture.CancellationToken);

        // Assert
        await actual.Should().ThrowAsync<ValidationException>();
    }
    
    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task SearchRepositoryAsync_ShouldThrowsValidationException_WhenLimitIsLowerThanOrEqualToZero(int limit)
    {
        // Arrange
        var searchRepositoryQuery = new SearchRepositoryQueryDto
        {
            Query = "fakeQuery",
            Page = 1,
            Limit = limit
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