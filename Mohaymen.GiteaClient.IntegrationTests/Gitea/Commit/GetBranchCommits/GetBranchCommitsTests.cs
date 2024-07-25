using System.Net;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.Gitea.Client.Abstractions;
using Mohaymen.GiteaClient.Gitea.Commit.GetBranchCommits.Dtos;
using Mohaymen.GiteaClient.IntegrationTests.Common.Collections.Gitea;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData.Abstractions;

namespace Mohaymen.GiteaClient.IntegrationTests.Gitea.Commit.GetBranchCommits;

[Collection("GiteaIntegrationTests")]
public class GetBranchCommitsTests : IClassFixture<GetBranchCommitsClassFixture>
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

    public GetBranchCommitsTests(GiteaCollectionFixture giteaCollectionFixture)
    {
        _giteaCollectionFixture =
            giteaCollectionFixture ?? throw new ArgumentNullException(nameof(giteaCollectionFixture));
        _giteaCollectionFixture = giteaCollectionFixture ?? throw new ArgumentNullException(nameof(giteaCollectionFixture));
        _testRepositoryCreator = giteaCollectionFixture.ServiceProvider.GetRequiredService<ITestRepositoryCreator>();
        _testBranchCreator = giteaCollectionFixture.ServiceProvider.GetRequiredService<ITestBranchCreator>();
        _testCommiter = giteaCollectionFixture.ServiceProvider.GetRequiredService<ITestCommiter>();
        _sut = _giteaCollectionFixture.ServiceProvider.GetRequiredService<IGiteaClient>();
    }
   
    public static TheoryData<int, int, List<string>> PageLimitCommitMessages()
    {
        return new TheoryData<int, int, List<string>>()
        {
            {
                1,
                1,
            },
            {
                2,
                1,
                ["Initial commit\n"]
            },
            {
                2,
                2,
                []
            },
            {
                1,
                10,
            }
        };
    }

    [Theory]
    [MemberData(nameof(PageLimitCommitMessages))]
    public async Task GetBranchCommitsAsync_ShouldReturnOkAndBranchCommitsBasedOnPageSizeAndLimitSize_WhenPageSizeAndLimitSizeAreSet(int page, int limit, List<string> expectedCommitMessages)
    {
        // Arrange 
        var loadBranchCommitsDto = new LoadBranchCommitsQueryDto
        {
            RepositoryName = RepositoryName,
            BranchName = BranchName,
            Page = page,
            Limit = limit
        };
        await _testRepositoryCreator.CreateRepositoryAsync(RepositoryName, _giteaCollectionFixture.CancellationToken);
        await _testBranchCreator.CreateBranchAsync(RepositoryName, BranchName, _giteaCollectionFixture.CancellationToken);
        await _testCommiter.CreateFileAsync(RepositoryName, BranchName, FilePath, CommitMessage, _giteaCollectionFixture.CancellationToken);
        
        // Act
        var actual = await _sut.CommitClient.LoadBranchCommitsAsync(loadBranchCommitsDto, _giteaCollectionFixture.CancellationToken);

        // Assert
        actual.StatusCode.Should().Be(HttpStatusCode.OK);
        var commitMessages = actual.Content!.Select(x => x.CommitDto.CommitMessage);
        commitMessages.Should().BeEquivalentTo(expectedCommitMessages);
    }
}