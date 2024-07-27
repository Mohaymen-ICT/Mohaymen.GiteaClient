using System.Net;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.Gitea.Client.Abstractions;
using Mohaymen.GiteaClient.Gitea.File.CreateFile.Dtos;
using Mohaymen.GiteaClient.Gitea.File.CreateFile.Models;
using Mohaymen.GiteaClient.IntegrationTests.Common.Collections.Gitea;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData.Abstractions;

namespace Mohaymen.GiteaClient.IntegrationTests.Gitea.File.CreateFile;

[Collection("GiteaIntegrationTests")]
public class CreateFileTests
{
    private readonly IGiteaClient _sut;
    private readonly ITestRepositoryCreator _repositoryCreator;
    private readonly GiteaCollectionFixture _giteaCollectionFixture;
    
    public CreateFileTests(GiteaCollectionFixture giteaCollectionFixture)
    {
        _giteaCollectionFixture = giteaCollectionFixture ?? throw new ArgumentNullException(nameof(giteaCollectionFixture));
        _repositoryCreator = _giteaCollectionFixture.ServiceProvider.GetRequiredService<ITestRepositoryCreator>();
        _sut = _giteaCollectionFixture.ServiceProvider.GetRequiredService<IGiteaClient>();
    }
    
    [Fact]
    public async Task CreateFile_ShouldCreateFileWithCreatedStatusCode_WhenInputsAreProvidedProperly()
    {
        // Arrange
        const string repositoryName = "create_file_repo";
        await _repositoryCreator.CreateRepositoryAsync(repositoryName, _giteaCollectionFixture.CancellationToken);

        const string filePath = "create_file.txt";
        const string content = "Hello, World!";
        const string encodedContent = "SGVsbG8sIFdvcmxkIQ==";
        var createFileCommandDto = new CreateFileCommandDto
        {
            RepositoryName = repositoryName,
            FilePath = filePath,
            Content = content,
            Author = new Identity
            {
                Email = "create_file@example.com",
                Name = "create_file_user"
            },
            CommitMessage = "create_file_test"
        };

        // Act
        var actual = await _sut.FileClient.CreateFileAsync(createFileCommandDto, _giteaCollectionFixture.CancellationToken);

        // Assert
        actual.StatusCode.Should().Be(HttpStatusCode.Created);
        actual.Content!.Content.FileName.Should().Be(filePath);
        actual.Content!.Content.FilePath.Should().Be(filePath);
        actual.Content!.Content.StringContent.Should().Be(encodedContent);
    }
}