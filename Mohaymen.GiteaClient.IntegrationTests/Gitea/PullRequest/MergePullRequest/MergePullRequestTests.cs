using System.Net;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.Gitea.Client.Abstractions;
using Mohaymen.GiteaClient.Gitea.File.CreateFile.Dtos;
using Mohaymen.GiteaClient.Gitea.PullRequest.Common.Enums;
using Mohaymen.GiteaClient.Gitea.PullRequest.CreatePullRequest.Dtos;
using Mohaymen.GiteaClient.Gitea.PullRequest.GetPullRequestList.Dtos;
using Mohaymen.GiteaClient.Gitea.PullRequest.MergePullRequest.Dtos;
using Mohaymen.GiteaClient.IntegrationTests.Common.Collections.Gitea;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData.Abstractions;
using Mohaymen.GiteaClient.IntegrationTests.Common.Models;

namespace Mohaymen.GiteaClient.IntegrationTests.Gitea.PullRequest.MergePullRequest;

[Collection("GiteaIntegrationTests")]
public class MergePullRequestTests
{
    private readonly IGiteaClient _sut;
    private readonly ITestRepositoryCreator _repositoryCreator;
    private readonly ITestBranchCreator _branchCreator;
    private readonly ITestPullRequestCreator _pullRequestCreator;
    private readonly ITestFileCreator _fileCreator;
    private readonly GiteaCollectionFixture _giteaCollectionFixture;
    
    public MergePullRequestTests(GiteaCollectionFixture giteaCollectionFixture)
    {
        _giteaCollectionFixture = giteaCollectionFixture ?? throw new ArgumentNullException(nameof(giteaCollectionFixture));
        _repositoryCreator = _giteaCollectionFixture.ServiceProvider.GetRequiredService<ITestRepositoryCreator>();
        _branchCreator = _giteaCollectionFixture.ServiceProvider.GetRequiredService<ITestBranchCreator>();
        _pullRequestCreator = _giteaCollectionFixture.ServiceProvider.GetRequiredService<ITestPullRequestCreator>();
        _fileCreator = _giteaCollectionFixture.ServiceProvider.GetRequiredService<ITestFileCreator>();
        _sut = giteaCollectionFixture.ServiceProvider.GetRequiredService<IGiteaClient>();
    }
    
    [Fact]
    public async Task MergePullRequest_ShouldMergePullRequest_WhenInputsAreProvidedProperly()
    {
        // Arrange
        const string repositoryName = "merge_pull_request_repo";
        const string headBranch = "merge_pull_request_branch";
        const string baseBranch = GiteaTestConstants.DefaultBranch;
        const string title = "merge_pull_request_title";
        const string assignee = GiteaTestConstants.Username;
        var createPullRequestCommandDto = new CreatePullRequestCommandDto
        {
            RepositoryName = repositoryName,
            HeadBranch = headBranch,
            BaseBranch = baseBranch,
            Title = title,
            Assignee = assignee
        };
        var cancellationToken = _giteaCollectionFixture.CancellationToken;
        const string filePath = "merge_pull_request_file.txt";
        const string content = "merge_pull_request_content.txt";
        var createFileCommandDto = new CreateFileCommandDto
        {
            RepositoryName = repositoryName,
            FilePath = filePath,
            Content = content,
            BranchName = headBranch
        };

        await _repositoryCreator.CreateRepositoryAsync(repositoryName, cancellationToken);
        await _branchCreator.CreateBranchAsync(repositoryName, headBranch, cancellationToken);
        await _fileCreator.CreateFileAsync(createFileCommandDto, cancellationToken);
        await _pullRequestCreator.CreatePullRequestAsync(createPullRequestCommandDto, cancellationToken);

        var mergePullRequestCommandDto = new MergePullRequestCommandDto
        {
            RepositoryName = repositoryName,
            Index = 1,
            MergeType = MergeType.Merge
        };
        var expectedGetPullRequestListCommandDto = new GetPullRequestListCommandDto
        {
            RepositoryName = repositoryName
        };

        // Act
        var actual = await _sut.PullRequestClient.MergePullRequestAsync(mergePullRequestCommandDto, cancellationToken);

        // Assert
        actual.StatusCode.Should().Be(HttpStatusCode.OK);
        var getPullRequestListResponse = await _sut.PullRequestClient.GetPullRequestListAsync(
            expectedGetPullRequestListCommandDto,
            _giteaCollectionFixture.CancellationToken);
        getPullRequestListResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        getPullRequestListResponse.Content.Should().NotBeNull();
        getPullRequestListResponse.Content!.First().Merged.Should().BeTrue();
    }
}