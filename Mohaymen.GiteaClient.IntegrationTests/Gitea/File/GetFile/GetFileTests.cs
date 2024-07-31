using System.Net;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.Gitea.Client.Abstractions;
using Mohaymen.GiteaClient.Gitea.File.CreateFile.Dtos;
using Mohaymen.GiteaClient.Gitea.File.CreateFile.Models;
using Mohaymen.GiteaClient.Gitea.File.GetRepositoryFile.Dtos;
using Mohaymen.GiteaClient.IntegrationTests.Common.Collections.Gitea;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData.Abstractions;

namespace Mohaymen.GiteaClient.IntegrationTests.Gitea.File.GetFile;

[Collection("GiteaIntegrationTests")]
public class GetFileTests
{
    private readonly IGiteaClient _sut;
    private readonly ITestRepositoryCreator _repositoryCreator;
    private readonly ITestFileCreator _fileCreator;
    private readonly GiteaCollectionFixture _giteaCollectionFixture;
    
    public GetFileTests(GiteaCollectionFixture giteaCollectionFixture)
    {
        _giteaCollectionFixture = giteaCollectionFixture ?? throw new ArgumentNullException(nameof(giteaCollectionFixture));
        _repositoryCreator = _giteaCollectionFixture.ServiceProvider.GetRequiredService<ITestRepositoryCreator>();
        _fileCreator = _giteaCollectionFixture.ServiceProvider.GetRequiredService<ITestFileCreator>();
        _sut = _giteaCollectionFixture.ServiceProvider.GetRequiredService<IGiteaClient>();
    }
    
    [Fact]
    public async Task GetFile_ShouldGetFileWithOkStatusCode_WhenInputsAreProvidedProperly()
    {
        // Arrange
        const string repositoryName = "get_file_repo";
        const string filePath = "get_file.txt";
        const string content = "Hello, World!";
        var cancellationToken = _giteaCollectionFixture.CancellationToken;
        await _repositoryCreator.CreateRepositoryAsync(repositoryName, cancellationToken);
        var createFileCommandDto = new CreateFileCommandDto
        {
            RepositoryName = repositoryName,
            FilePath = filePath,
            Content = content
        };
        await _fileCreator.CreateFileAsync(createFileCommandDto, cancellationToken);

        var getFileCommandDto = new GetFileCommandDto
        {
            RepositoryName = repositoryName,
            FilePath = filePath
        };

        // Act
        var actual = await _sut.FileClient.GetFileAsync(getFileCommandDto, cancellationToken);

        // Assert
        actual.StatusCode.Should().Be(HttpStatusCode.OK);
        actual.Content?.Content.Should().Be(content);
        actual.Content?.FilePath.Should().Be(filePath);
        actual.Content?.FileName.Should().Be(filePath);
    }
}