using MediatR;
using Microsoft.Extensions.Options;
using Mohaymen.GiteaClient.Core.Configs;
using Mohaymen.GiteaClient.Gitea.PullRequest.Common.ApiCall.Abstractions;
using Mohaymen.GiteaClient.Gitea.PullRequest.Common.Enums;
using Mohaymen.GiteaClient.Gitea.PullRequest.Common.Enums.Extensions;
using Mohaymen.GiteaClient.Gitea.PullRequest.GetPullRequestList.Commands;
using Mohaymen.GiteaClient.Gitea.PullRequest.GetPullRequestList.Context;
using Mohaymen.GiteaClient.Gitea.PullRequest.GetPullRequestList.Dtos;
using NSubstitute;
using Refit;
using Xunit;

namespace Mohaymen.GiteaClient.Tests.Gitea.PullRequest.GetPullRequestList.Commands;

public class GetPullRequestListCommandHandlerTests
{
    private readonly IPullRequestRestClient _pullRequestRestClient;
    private readonly IOptions<GiteaApiConfiguration> _options;
    private readonly IRequestHandler<GetPullRequestListCommand, ApiResponse<List<GetPullRequestListResponseDto>>> _sut;

    public GetPullRequestListCommandHandlerTests()
    {
        _pullRequestRestClient = Substitute.For<IPullRequestRestClient>();
        _options = Substitute.For<IOptions<GiteaApiConfiguration>>();
        _sut = new GetPullRequestListCommandHandler(_pullRequestRestClient, _options);
    }

    [Fact]
    public async Task Handle_ShouldCallGetPullRequestListAsync_AndInputsAreValid()
    {
        // Arrange
        const string owner = "owner";
        const string repositoryName = "repo";
        const PullRequestState pullRequestState = PullRequestState.Open;
        const SortCriteria sortCriteria = SortCriteria.RecentUpdate;
        var labelIds = new List<int>
        {
            1, 2, 3
        };
        var command = new GetPullRequestListCommand
        {
            RepositoryName = repositoryName,
            State = pullRequestState,
            SortBy = sortCriteria,
            LabelIds = labelIds
        };
        _options.Value.Returns(new GiteaApiConfiguration
        {
            BaseUrl = "url",
            PersonalAccessToken = "token",
            RepositoriesOwner = owner
        });
        var expected = new GetPullRequestListRequest
        {
            State = pullRequestState.Normalize(),
            SortBy = sortCriteria.Normalize(),
            LabelIds = labelIds
        };

        // Act
        await _sut.Handle(command, default);
        
        // Assert
        await _pullRequestRestClient.Received(1).GetPullRequestListAsync(owner,
            repositoryName,
            Arg.Is<GetPullRequestListRequest>(x => x.Equals(expected)),
            default);
    }
}