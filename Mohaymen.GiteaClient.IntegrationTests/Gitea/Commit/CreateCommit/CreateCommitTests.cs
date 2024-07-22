using System.Net;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.Gitea.Client.Abstractions;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Dtos.Request;
using Mohaymen.GiteaClient.IntegrationTests.Common.Assertions.Abstractions;
using Mohaymen.GiteaClient.IntegrationTests.Common.Collections.Gitea;

namespace Mohaymen.GiteaClient.IntegrationTests.Gitea.Commit.CreateCommit;

[Collection("GiteaIntegrationTests")]
public class CreateCommitTests : IClassFixture<CreateCommitClassFixture>
{
    private readonly IGiteaClient _sut;
    private readonly ITestCommitChecker _testCommitChecker;
    private readonly ITestFileChecker _testFileChecker;
    private readonly GiteaCollectionFixture _giteaCollectionFixture;

    public CreateCommitTests(GiteaCollectionFixture giteaCollectionFixture)
    {
        _giteaCollectionFixture = giteaCollectionFixture ?? throw new ArgumentNullException(nameof(giteaCollectionFixture));
        _sut = _giteaCollectionFixture.ServiceProvider.GetRequiredService<IGiteaClient>();
        _testFileChecker = giteaCollectionFixture.ServiceProvider.GetRequiredService<ITestFileChecker>();
        _testCommitChecker = giteaCollectionFixture.ServiceProvider.GetRequiredService<ITestCommitChecker>();
    }

    [Fact]
    public async Task CreateCommitAsync_ShouldCreateCommitAndAddFileWithItsContent_WhenCommitIsForFileCreating()
    {
        // Arrange
        const string filePath = "sample.txt";
        const string content = "alireza eiji is the man of focus!";
        const string expectedContent = "YWxpcmV6YSBlaWppIGlzIHRoZSBtYW4gb2YgZm9jdXMh";
        var createCommitCommandDto = new CreateCommitCommandDto
        {
            RepositoryName = CreateCommitClassFixture.RepositoryName,
            BranchName = "main",
            CommitMessage = "some trivial commit",
            FileDtos =
            [
                new FileCommitDto
                {
                    Path = filePath,
                    Content = content,
                    CommitActionDto = CommitActionDto.Create
                }
            ]
        };

        // Act
        var actual = await _sut.CommitClient.CreateCommitAsync(createCommitCommandDto, _giteaCollectionFixture.CancellationToken);
        
        // Assert
        actual.StatusCode.Should().Be(HttpStatusCode.Created);
        var commitSha = actual.Content!.CommitResponseDto.Sha;
        var isCommitCreated= await _testCommitChecker.ContainsCommitWithShaAsync(CreateCommitClassFixture.RepositoryName,
            "main",
            commitSha,
            _giteaCollectionFixture.CancellationToken);
        isCommitCreated.Should().BeTrue();
        var isFileExists = await _testFileChecker.ContainsFileAsync(CreateCommitClassFixture.RepositoryName,
            filePath,
            _giteaCollectionFixture.CancellationToken);
        isFileExists.Should().BeTrue();
        var doesFileHaveContent = await _testFileChecker.HasFileContent(CreateCommitClassFixture.RepositoryName,
            filePath,
            expectedContent,
            _giteaCollectionFixture.CancellationToken);
        doesFileHaveContent.Should().BeTrue();
    }
    
    [Fact]
    public async Task CreateCommitAsync_ShouldCreateCommitAndEditFileWithItsContent_WhenCommitIsForFileEditing()
    {
        // Arrange
        const string filePath = "README.md";
        const string content = "alireza eiji is the man of focus!";
        const string expectedContent = "YWxpcmV6YSBlaWppIGlzIHRoZSBtYW4gb2YgZm9jdXMh";
        var createCommitCommandDto = new CreateCommitCommandDto
        {
            RepositoryName = CreateCommitClassFixture.RepositoryName,
            BranchName = "main",
            CommitMessage = "some trivial commit",
            FileDtos =
            [
                new FileCommitDto
                {
                    Path = filePath,
                    Content = content,
                    CommitActionDto = CommitActionDto.Update
                }
            ]
        };

        // Act
        var actual = await _sut.CommitClient.CreateCommitAsync(createCommitCommandDto, _giteaCollectionFixture.CancellationToken);
        
        // Assert
        actual.StatusCode.Should().Be(HttpStatusCode.Created);
        var commitSha = actual.Content!.CommitResponseDto.Sha;
        var isCommitCreated= await _testCommitChecker.ContainsCommitWithShaAsync(CreateCommitClassFixture.RepositoryName,
            "main",
            commitSha,
            _giteaCollectionFixture.CancellationToken);
        isCommitCreated.Should().BeTrue();
        var isFileExists = await _testFileChecker.ContainsFileAsync(CreateCommitClassFixture.RepositoryName,
            filePath,
            _giteaCollectionFixture.CancellationToken);
        isFileExists.Should().BeTrue();
        var doesFileHaveContent = await _testFileChecker.HasFileContent(CreateCommitClassFixture.RepositoryName,
            filePath,
            expectedContent,
            _giteaCollectionFixture.CancellationToken);
        doesFileHaveContent.Should().BeTrue();
    }
    
    [Fact]
    public async Task CreateCommitAsync_ShouldCreateCommitAndDeleteFileWithItsContent_WhenCommitIsForFileDeleting()
    {
        // Arrange
        var createCommitCommandDto = new CreateCommitCommandDto
        {
            RepositoryName = CreateCommitClassFixture.RepositoryName,
            BranchName = "main",
            CommitMessage = "some trivial commit",
            FileDtos =
            [
                new FileCommitDto
                {
                    Path = CreateCommitClassFixture.FileDelete,
                    Content = "",
                    CommitActionDto = CommitActionDto.Delete
                }
            ]
        };

        // Act
        var actual = await _sut.CommitClient.CreateCommitAsync(createCommitCommandDto, _giteaCollectionFixture.CancellationToken);
        
        // Assert
        actual.StatusCode.Should().Be(HttpStatusCode.Created);
        var commitSha = actual.Content!.CommitResponseDto.Sha;
        var isCommitCreated= await _testCommitChecker.ContainsCommitWithShaAsync(CreateCommitClassFixture.RepositoryName,
            "main",
            commitSha,
            _giteaCollectionFixture.CancellationToken);
        isCommitCreated.Should().BeTrue();
        var isFileExists = await _testFileChecker.ContainsFileAsync(CreateCommitClassFixture.RepositoryName,
            CreateCommitClassFixture.FileDelete,
            _giteaCollectionFixture.CancellationToken);
        isFileExists.Should().BeFalse();
    }
}