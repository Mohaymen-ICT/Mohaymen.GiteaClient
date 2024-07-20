using System.Net;
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.Gitea.Client.Abstractions;
using Mohaymen.GiteaClient.Gitea.Repository.DeleteRepository.Dto;
using Mohaymen.GiteaClient.IntegrationTests.Common.Collections.Gitea;

namespace Mohaymen.GiteaClient.IntegrationTests.Gitea.Repository.DeleteRepository;

[Collection("GiteaIntegrationTests")]
public class DeleteRepositoryTests : IClassFixture<RepositoryClassFixture>
{
    private readonly IGiteaClient _sut;
    private readonly GiteaCollectionFixture _giteaCollectionFixture;

    public DeleteRepositoryTests(GiteaCollectionFixture giteaCollectionFixture)
    {
        _giteaCollectionFixture = giteaCollectionFixture ?? throw new ArgumentNullException(nameof(giteaCollectionFixture));
        _sut = _giteaCollectionFixture.ServiceProvider.GetRequiredService<IGiteaClient>();
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task DeleteRepositoryAsync_ShouldThrowsValidationException_WhenRepositoryNameIsNullOrEmpty(string repositoryName)
    {
        // Arrange
        var deleteRepositoryCommandDto = new DeleteRepositoryCommandDto
        {
            RepositoryName = repositoryName
        };

        // Act
        var actual = async () => await _sut.RepositoryClient.DeleteRepositoryAsync(deleteRepositoryCommandDto, _giteaCollectionFixture.CancellationToken);

        // Assert
        await actual.Should().ThrowAsync<ValidationException>();
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
    }

    [Fact]
    public async Task DeleteRepositoryAsync_ShouldReturnNotFound_WhenRepositoryDoesNotExist()
    {
        // Arrange
        var deletedRepositoryCommandDto = new DeleteRepositoryCommandDto
        {
            RepositoryName = "sampleFakeRepo"
        };
        
        // Act
        var actual = await _sut.RepositoryClient.DeleteRepositoryAsync(deletedRepositoryCommandDto, _giteaCollectionFixture.CancellationToken);

        // Assert
        actual.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}