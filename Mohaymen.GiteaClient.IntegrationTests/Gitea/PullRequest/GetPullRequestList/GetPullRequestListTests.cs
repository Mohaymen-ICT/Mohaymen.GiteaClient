using System.Net;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.Gitea.Client.Abstractions;
using Mohaymen.GiteaClient.Gitea.PullRequest.CreatePullRequest.Dtos;
using Mohaymen.GiteaClient.Gitea.PullRequest.GetPullRequestList.Dtos;
using Mohaymen.GiteaClient.IntegrationTests.Common.Collections.Gitea;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData.Abstractions;
using Mohaymen.GiteaClient.IntegrationTests.Common.Models;

namespace Mohaymen.GiteaClient.IntegrationTests.Gitea.PullRequest.GetPullRequestList;

[Collection("GiteaIntegrationTests")]
public class GetPullRequestListTests
{
    private readonly IGiteaClient _sut;
    private readonly ITestRepositoryCreator _repositoryCreator;
    private readonly ITestBranchCreator _branchCreator;
    private readonly ITestPullRequestCreator _pullRequestCreator;
    private readonly GiteaCollectionFixture _giteaCollectionFixture;
    
    public GetPullRequestListTests(GiteaCollectionFixture giteaCollectionFixture)
    {
        _giteaCollectionFixture = giteaCollectionFixture ?? throw new ArgumentNullException(nameof(giteaCollectionFixture));
        _repositoryCreator = _giteaCollectionFixture.ServiceProvider.GetRequiredService<ITestRepositoryCreator>();
        _branchCreator = _giteaCollectionFixture.ServiceProvider.GetRequiredService<ITestBranchCreator>();
        _pullRequestCreator = _giteaCollectionFixture.ServiceProvider.GetRequiredService<ITestPullRequestCreator>();
        _sut = giteaCollectionFixture.ServiceProvider.GetRequiredService<IGiteaClient>();
    }
    
    [Fact]
    public async Task CreatePullRequest_ShouldGetBranchListOfRepo_WhenInputsAreProvidedProperly()
    {
        // Arrange
        const string repositoryName = "get_pull_request_list_repo";
        const string headBranch = "get_pull_request_list_branch";
        const string baseBranch = GiteaTestConstants.DefaultBranch;
        const string body = "get_pull_request_list_body";
        const string title = "get_pull_request_list_title";
        const string assignee = GiteaTestConstants.Username;
        var createPullRequestCommandDto = new CreatePullRequestCommandDto
        {
            RepositoryName = repositoryName,
            HeadBranch = headBranch,
            BaseBranch = baseBranch,
            Body = body,
            Title = title,
            Assignee = assignee
        };
        var cancellationToken = _giteaCollectionFixture.CancellationToken;

        await _repositoryCreator.CreateRepositoryAsync(repositoryName, cancellationToken);
        await _branchCreator.CreateBranchAsync(repositoryName, headBranch, cancellationToken);
        await _pullRequestCreator.CreatePullRequestAsync(createPullRequestCommandDto, cancellationToken);

        var getPullRequestListCommandDto = new GetPullRequestListCommandDto
        {
            RepositoryName = repositoryName
        };

        // Act
        var actual = await _sut.PullRequestClient.GetPullRequestListAsync(getPullRequestListCommandDto, cancellationToken);

        // Assert
        actual.StatusCode.Should().Be(HttpStatusCode.OK);
        actual.Content?.Should().ContainSingle();
        var responsePullRequest = actual.Content?.First()!;
        responsePullRequest.Title.Should().Be(title);
        responsePullRequest.Body.Should().Be(body);
        responsePullRequest.HeadBranch.Should().BeEquivalentTo(new GiteaClient.Gitea.PullRequest.Common.Models.Branch
        {
            Name = headBranch
        });
        responsePullRequest.BaseBranch.Should().BeEquivalentTo(new GiteaClient.Gitea.PullRequest.Common.Models.Branch
        {
            Name = baseBranch
        });
        responsePullRequest.Assignees.Should().ContainSingle(x => x.Email == $"{assignee}@noreply.localhost");
        responsePullRequest.Merged.Should().BeFalse();
    }
}