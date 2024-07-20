using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using Mohaymen.GiteaClient.Core.Configs;
using Mohaymen.GiteaClient.Gitea.Commit.Common.ApiCall;
using Mohaymen.GiteaClient.Gitea.Commit.GetBranchCommits.Dtos;
using Mohaymen.GiteaClient.Gitea.Commit.GetBranchCommits.Queries;
using NSubstitute;
using Refit;
using Xunit;

namespace Mohaymen.GiteaClient.Tests.Gitea.Commit.GetBranchCommits.Queries;

public class LoadBranchCommitsQueryTests
{
    private readonly InlineValidator<LoadBranchCommitsQuery> _validator;
    private readonly ICommitRestClient _commitRestClient;
    private readonly IOptions<GiteaApiConfiguration> _giteaOptions;
    private readonly IRequestHandler<LoadBranchCommitsQuery, ApiResponse<List<LoadBranchCommitsResponseDto>>> _sut;

    public LoadBranchCommitsQueryTests()
    {
        _validator = new InlineValidator<LoadBranchCommitsQuery>();
        _commitRestClient = Substitute.For<ICommitRestClient>();
        _giteaOptions = Substitute.For<IOptions<GiteaApiConfiguration>>();
        _sut = new LoadBranchCommitsQueryHandler(_validator, _commitRestClient, _giteaOptions);
    }

    [Fact]
    public async Task Handle_ShouldThrowsValidationException_WhenInputIsNotValid()
    {
        // Arrange
        var loadBranchCommitsQuery = new LoadBranchCommitsQuery
        {
            RepositoryName = "fakeRepo",
            BranchName = "fakeBranch"
        };
        _validator.RuleFor(x => x).Must(x => false);

        // Act
        var actual = async () => await _sut.Handle(loadBranchCommitsQuery, default);

        // Assert
        await actual.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task Handle_ShouldCallLoadBranchCommitsAsync_WhenInputIsValid()
    {
        // Arrange
        const string owner = "fakeOwner";
        const string repoName = "fakeRepoName";
        const string branchName = "fakeBranchName";
        const int page = 1;
        const int limit = 10;
        var loadBranchCommitsQuery = new LoadBranchCommitsQuery
        {
            RepositoryName = repoName,
            BranchName = branchName,
            Page = page,
            Limit = limit
        };
        var gitaApiConfig = new GiteaApiConfiguration
        {
            BaseUrl = "fakeUrl",
            PersonalAccessToken = "fakeToken",
            RepositoriesOwner = owner
        };
        _giteaOptions.Value.Returns(gitaApiConfig);
        
        // Act
        await _sut.Handle(loadBranchCommitsQuery, default);

        // Assert
        await _commitRestClient.Received(1).LoadBranchCommitsAsync(owner,
            repoName,
            branchName,
            page,
            limit);
    }
}