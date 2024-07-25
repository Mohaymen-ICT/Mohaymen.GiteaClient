using System.Net;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.Gitea.Client.Abstractions;
using Mohaymen.GiteaClient.Gitea.Commit.GetBranchCommits.Dtos;
using Mohaymen.GiteaClient.IntegrationTests.Common.Collections.Gitea;

namespace Mohaymen.GiteaClient.IntegrationTests.Gitea.Commit.GetBranchCommits;

[Collection("GiteaIntegrationTests")]
public class GetBranchCommitsTests : IClassFixture<GetBranchCommitsClassFixture>
{
    private readonly IGiteaClient _sut;
    private readonly GiteaCollectionFixture _giteaCollectionFixture;

    public GetBranchCommitsTests(GiteaCollectionFixture giteaCollectionFixture)
    {
        _giteaCollectionFixture = giteaCollectionFixture ?? throw new ArgumentNullException(nameof(giteaCollectionFixture));
        _sut = _giteaCollectionFixture.ServiceProvider.GetRequiredService<IGiteaClient>();
    }
   
    public static TheoryData<int, int, List<string>> PageLimitCommitMessages()
    {
        return new TheoryData<int, int, List<string>>()
        {
            {
                1,
                1,
                [$"{GetBranchCommitsClassFixture.CommitMessage}\n"]
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
                [$"{GetBranchCommitsClassFixture.CommitMessage}\n", "Initial commit\n"]
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
            RepositoryName = GetBranchCommitsClassFixture.RepositoryName,
            BranchName = GetBranchCommitsClassFixture.BranchName,
            Page = page,
            Limit = limit
        };

        // Act
        var actual = await _sut.CommitClient.LoadBranchCommitsAsync(loadBranchCommitsDto, _giteaCollectionFixture.CancellationToken);

        // Assert
        actual.StatusCode.Should().Be(HttpStatusCode.OK);
        var commitMessages = actual.Content!.Select(x => x.CommitDto.CommitMessage);
        commitMessages.Should().BeEquivalentTo(expectedCommitMessages);
    }
}