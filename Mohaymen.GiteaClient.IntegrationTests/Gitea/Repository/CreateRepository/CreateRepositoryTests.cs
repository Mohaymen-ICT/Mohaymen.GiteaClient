using System.Net;
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.Gitea.Client.Abstractions;
using Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Dtos;
using Mohaymen.GiteaClient.IntegrationTests.Common.Collections.Gitea;

namespace Mohaymen.GiteaClient.IntegrationTests.Gitea.Repository.CreateRepository;

[Collection("GiteaIntegrationTests")]
public class CreateRepositoryTests
{
    private readonly IGiteaClient _sut;
    private readonly GiteaCollectionFixture _giteaCollectionFixture;
    
    public CreateRepositoryTests(GiteaCollectionFixture giteaCollectionFixture)
    {
        _giteaCollectionFixture = giteaCollectionFixture ?? throw new ArgumentNullException(nameof(giteaCollectionFixture));
        _sut = giteaCollectionFixture.ServiceProvider.GetRequiredService<IGiteaClient>();
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task CreateRepository_ShouldThrowsValidationException_WhenRepositoryNameIsNullOrEmpty(string repositoryName)
    {
        // Arrange
        var createRepositoryCommandDto = new CreateRepositoryCommandDto
        {
            Name = repositoryName,
            DefaultBranch = "main"
        };

        // Act
        var actual = async () => await _sut.RepositoryClient.CreateRepositoryAsync(createRepositoryCommandDto, _giteaCollectionFixture.CancellationToken);

        // Assert
        await actual.Should().ThrowAsync<ValidationException>();
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task CreateRepository_ShouldThrowsValidationException_WhenDefaultBranchNameIsNullOrEmpty(string branchName)
    {
        // Arrange
        var createRepositoryCommandDto = new CreateRepositoryCommandDto
        {
            Name = "FooRepo",
            DefaultBranch = branchName
        };
        
        // Act
        var actual = async () => await _sut.RepositoryClient.CreateRepositoryAsync(createRepositoryCommandDto, _giteaCollectionFixture.CancellationToken);
        
        // Assert
        await actual.Should().ThrowAsync<ValidationException>();
    }
    
    [Fact]
    public async Task CreateRepository_ShouldCreateRepositoryWithCreatedStatusCode_WhenInputsAreProvidedProperly()
    {
        // Arrange
        const string repositoryName = "CreateTestRepo";
        var createRepositoryCommandDto = new CreateRepositoryCommandDto
        {
            Name = repositoryName,
            DefaultBranch = "main",
            IsPrivateBranch = true
        };
        
        // Act
        var actual = await _sut.RepositoryClient.CreateRepositoryAsync(createRepositoryCommandDto, _giteaCollectionFixture.CancellationToken);

        // Assert
        actual.StatusCode.Should().Be(HttpStatusCode.Created);
        actual.Content!.RepositoryName.Should().Be(repositoryName);
    }
}