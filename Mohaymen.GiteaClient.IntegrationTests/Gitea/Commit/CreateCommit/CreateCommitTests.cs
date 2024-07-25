using System.Net;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.Gitea.Client.Abstractions;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Dtos.Request;
using Mohaymen.GiteaClient.IntegrationTests.Common.Assertions.Abstractions;
using Mohaymen.GiteaClient.IntegrationTests.Common.Collections.Gitea;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData.Abstractions;

namespace Mohaymen.GiteaClient.IntegrationTests.Gitea.Commit.CreateCommit;

[Collection("GiteaIntegrationTests")]
public class CreateCommitTests
{
    private const string RepositoryName = "CreateCommitRepository";
    private readonly IGiteaClient _sut;
    private readonly ITestCommitChecker _testCommitChecker;
    private readonly ITestFileChecker _testFileChecker;
    private readonly ITestRepositoryCreator _testRepositoryCreator;
    private readonly ITestCommiter _testCommiter;
    private readonly GiteaCollectionFixture _giteaCollectionFixture;

    public CreateCommitTests(GiteaCollectionFixture giteaCollectionFixture)
    {
        _giteaCollectionFixture = giteaCollectionFixture ?? throw new ArgumentNullException(nameof(giteaCollectionFixture));
        _sut = _giteaCollectionFixture.ServiceProvider.GetRequiredService<IGiteaClient>();
        _testFileChecker = giteaCollectionFixture.ServiceProvider.GetRequiredService<ITestFileChecker>();
        _testCommitChecker = giteaCollectionFixture.ServiceProvider.GetRequiredService<ITestCommitChecker>();
        _testRepositoryCreator = giteaCollectionFixture.ServiceProvider.GetRequiredService<ITestRepositoryCreator>();
        _testCommiter = giteaCollectionFixture.ServiceProvider.GetRequiredService<ITestCommiter>();
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
            RepositoryName = RepositoryName,
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
        var isCommitCreated= await _testCommitChecker.ContainsCommitWithShaAsync(RepositoryName,
            "main",
            commitSha,
            _giteaCollectionFixture.CancellationToken);
        isCommitCreated.Should().BeTrue();
        var isFileExists = await _testFileChecker.ContainsFileAsync(RepositoryName,
            filePath,
            _giteaCollectionFixture.CancellationToken);
        isFileExists.Should().BeTrue();
        var doesFileHaveContent = await _testFileChecker.HasFileContent(RepositoryName,
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
            RepositoryName = RepositoryName,
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
        var isCommitCreated= await _testCommitChecker.ContainsCommitWithShaAsync(RepositoryName,
            "main",
            commitSha,
            _giteaCollectionFixture.CancellationToken);
        isCommitCreated.Should().BeTrue();
        var isFileExists = await _testFileChecker.ContainsFileAsync(RepositoryName,
            filePath,
            _giteaCollectionFixture.CancellationToken);
        isFileExists.Should().BeTrue();
        var doesFileHaveContent = await _testFileChecker.HasFileContent(RepositoryName,
            filePath,
            expectedContent,
            _giteaCollectionFixture.CancellationToken);
        doesFileHaveContent.Should().BeTrue();
    }
    
    [Fact]
    public async Task CreateCommitAsync_ShouldCreateCommitAndDeleteFileWithItsContent_WhenCommitIsForFileDeleting()
    {
        // Arrange
        const string FileDelete = "SampleDeleteFile.txt";
        var createCommitCommandDto = new CreateCommitCommandDto
        {
            RepositoryName = RepositoryName,
            BranchName = "main",
            CommitMessage = "some trivial commit",
            FileDtos =
            [
                new FileCommitDto
                {
                    Path = FileDelete,
                    Content = "",
                    CommitActionDto = CommitActionDto.Delete
                }
            ]
        };
        
        await _testRepositoryCreator.CreateRepositoryAsync(RepositoryName, _giteaCollectionFixture.CancellationToken);
        await _testCommiter.CreateFileAsync(RepositoryName,
            "main",
            FileDelete,
            "ccc",
            _giteaCollectionFixture.CancellationToken);

        // Act
        var actual = await _sut.CommitClient.CreateCommitAsync(createCommitCommandDto, _giteaCollectionFixture.CancellationToken);
        
        // Assert
        actual.StatusCode.Should().Be(HttpStatusCode.Created);
        var commitSha = actual.Content!.CommitResponseDto.Sha;
        var isCommitCreated= await _testCommitChecker.ContainsCommitWithShaAsync(RepositoryName,
            "main",
            commitSha,
            _giteaCollectionFixture.CancellationToken);
        isCommitCreated.Should().BeTrue();
        var isFileExists = await _testFileChecker.ContainsFileAsync(RepositoryName,
            FileDelete,
            _giteaCollectionFixture.CancellationToken);
        isFileExists.Should().BeFalse();
    }
}