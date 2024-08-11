using System.Net;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.Gitea.Client.Abstractions;
using Mohaymen.GiteaClient.Gitea.Commit.GetBranchCommits.Dtos;
using Mohaymen.GiteaClient.IntegrationTests.Common.Collections.Gitea;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData.Abstractions;

namespace Mohaymen.GiteaClient.IntegrationTests.Gitea.Commit.GetSingleCommit;

[Collection("GiteaIntegrationTests")]
public class GetSingleCommitTests
{
    private const string RepositoryName = "GetBranchCommitsRepo";
    private const string BranchName = "feature/IntegrationTest";
    private const string FilePath = "README1.md";
    private const string CommitMessage = "fakeCommitMessage";
    private readonly ITestRepositoryCreator _testRepositoryCreator;
    private readonly ITestBranchCreator _testBranchCreator;
    private readonly ITestCommiter _testCommiter;
    private readonly IGiteaClient _sut;
    private readonly GiteaCollectionFixture _giteaCollectionFixture;

    public GetSingleCommitTests(GiteaCollectionFixture giteaCollectionFixture)
    {
        _giteaCollectionFixture =
            giteaCollectionFixture ?? throw new ArgumentNullException(nameof(giteaCollectionFixture));
        _testRepositoryCreator = giteaCollectionFixture.ServiceProvider.GetRequiredService<ITestRepositoryCreator>();
        _testBranchCreator = giteaCollectionFixture.ServiceProvider.GetRequiredService<ITestBranchCreator>();
        _testCommiter = giteaCollectionFixture.ServiceProvider.GetRequiredService<ITestCommiter>();
        _sut = _giteaCollectionFixture.ServiceProvider.GetRequiredService<IGiteaClient>();
    }

    [Fact]
    public async Task
        GetBranchCommitsAsync_ShouldReturnOkAndGetStatusOfFiles_WhenCallCommitByCommitSha()
    {
        // Arrange 
        await _testRepositoryCreator.CreateRepositoryAsync(RepositoryName, _giteaCollectionFixture.CancellationToken);
        await _testBranchCreator.CreateBranchAsync(RepositoryName, BranchName,
            _giteaCollectionFixture.CancellationToken);
        var commitSha = await _testCommiter.CreateFileAsync(RepositoryName, BranchName,
            FilePath, CommitMessage,
            _giteaCollectionFixture.CancellationToken);

        var getSingleCommitQueryDto = new GetSingleCommitQueryDto()
        {
            RepositoryName = RepositoryName,
            CommitSha = commitSha!.Content!.CommitResponseDto.Sha!
        };
        // Act
        var actual =
            await _sut.CommitClient.GetSingleCommitAsync(getSingleCommitQueryDto,
                _giteaCollectionFixture.CancellationToken);

        // Assert
        actual.StatusCode.Should().Be(HttpStatusCode.OK);
        actual.Content!.FilesDto[0].Status.Should().Be("added");
        actual.Content.StatsDto.Additions.Should().Be(1);
    }
}