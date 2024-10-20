namespace Mohaymen.GiteaClient.Tests.Gitea.Commit.GetSingleCommit.Queries;

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

public class GetSingleCommitQueryTests
{
    private readonly InlineValidator<GetSingleCommitQuery> _validator;
    private readonly ICommitRestClient _commitRestClient;
    private readonly IOptions<GiteaApiConfiguration> _giteaOptions;
    private readonly IRequestHandler<GetSingleCommitQuery, ApiResponse<GetSingleCommitResponseDto>> _sut;

    public GetSingleCommitQueryTests()
    {
        _validator = new InlineValidator<GetSingleCommitQuery>();
        _commitRestClient = Substitute.For<ICommitRestClient>();
        _giteaOptions = Substitute.For<IOptions<GiteaApiConfiguration>>();
        _sut = new GetSingleCommitQueryHandler(_validator, _commitRestClient, _giteaOptions);
    }

    [Fact]
    public async Task Handle_ShouldThrowsValidationException_WhenInputIsNotValid()
    {
        // Arrange
        var getSingleCommitQuery = new GetSingleCommitQuery()
        {
            RepositoryName = "fakeRepo",
            CommitSha = "fakeSha"
        };
        _validator.RuleFor(x => x).Must(x => false);

        // Act
        var actual = async () => await _sut.Handle(getSingleCommitQuery, default);

        // Assert
        await actual.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task Handle_ShouldGetSingleCommitAsync_WhenInputIsValid()
    {
        // Arrange
        const string owner = "fakeOwner";
        const string repoName = "fakeRepoName";
        const string? commitSha = "fakeSha";
        var getSingleCommitQuery = new GetSingleCommitQuery()
        {
            RepositoryName = repoName,
            CommitSha = commitSha
        };
        var gitaApiConfig = new GiteaApiConfiguration
        {
            BaseUrl = "fakeUrl",
            PersonalAccessToken = "fakeToken",
            RepositoriesOwner = owner
        };
        _giteaOptions.Value.Returns(gitaApiConfig);

        // Act
        await _sut.Handle(getSingleCommitQuery, default);

        // Assert
        await _commitRestClient.Received(1).GetSingleCommitAsync(owner,
            repoName,
            commitSha);
    }
}