using System.Net;
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.Gitea.Client.Abstractions;
using Mohaymen.GiteaClient.Gitea.Repository.DeleteRepository.Dto;
using Mohaymen.GiteaClient.IntegrationTests.Common.Assertions.Abstractions;
using Mohaymen.GiteaClient.IntegrationTests.Common.Collections.Gitea;

namespace Mohaymen.GiteaClient.IntegrationTests.Gitea.Repository.DeleteRepository;

[Collection("GiteaIntegrationTests")]
public class DeleteRepositoryTests : IClassFixture<RepositoryClassFixture>
{
    private readonly IGiteaClient _sut;
    private readonly ITestRepositoryChecker _testRepositoryChecker;
    private readonly GiteaCollectionFixture _giteaCollectionFixture;

    public DeleteRepositoryTests(GiteaCollectionFixture giteaCollectionFixture)
    {
        _giteaCollectionFixture = giteaCollectionFixture ?? throw new ArgumentNullException(nameof(giteaCollectionFixture));
        _testRepositoryChecker = _giteaCollectionFixture.ServiceProvider.GetRequiredService<ITestRepositoryChecker>();
        _sut = _giteaCollectionFixture.ServiceProvider.GetRequiredService<IGiteaClient>();
    }
    
    [Fact]
    public async Task DeleteRepositoryAsync_ShouldReturnNotContentSuccessResponse_WhenRepositoryExistsAndDeleted()
    {
        // Arrange
        var deletedRepositoryCommandDto = new DeleteRepositoryCommandDto
        {
            RepositoryName = RepositoryClassFixture.DeleteRepositoryName 
        };

        // Act
        var actual = await _sut.RepositoryClient.DeleteRepositoryAsync(deletedRepositoryCommandDto, _giteaCollectionFixture.CancellationToken);

        // Assert
        actual.StatusCode.Should().Be(HttpStatusCode.NoContent);
        var isRepositoryExists = await _testRepositoryChecker.ContainsRepositoryAsync(RepositoryClassFixture.DeleteRepositoryName, _giteaCollectionFixture.CancellationToken);
        isRepositoryExists.Should().BeFalse();
    }

    [Fact]
    public async Task DeleteRepositoryAsync_ShouldReturnNotFound_WhenRepositoryDoesNotExist()
    {
        // Arrange
        const string repositoryName = "sampleFakeRepo";
        var deletedRepositoryCommandDto = new DeleteRepositoryCommandDto
        {
            RepositoryName = repositoryName
        };
        
        // Act
        var actual = await _sut.RepositoryClient.DeleteRepositoryAsync(deletedRepositoryCommandDto, _giteaCollectionFixture.CancellationToken);

        // Assert
        actual.StatusCode.Should().Be(HttpStatusCode.NotFound);
        var isRepositoryExists = await _testRepositoryChecker.ContainsRepositoryAsync(repositoryName, _giteaCollectionFixture.CancellationToken);
        isRepositoryExists.Should().BeFalse();
    }
}