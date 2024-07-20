using MediatR;
using Mohaymen.GiteaClient.Gitea.Commit.Common.Facades;
using Mohaymen.GiteaClient.Gitea.Commit.Common.Facades.Abstractions;
using Mohaymen.GiteaClient.Gitea.Commit.GetBranchCommits.Dtos;
using Mohaymen.GiteaClient.Gitea.Commit.GetBranchCommits.Queries;
using NSubstitute;
using Xunit;

namespace Mohaymen.GiteaClient.Tests.Gitea.Commit.Common.Facades;

public class CommitFacadeTests
{
    private readonly IMediator _mediator;
    private readonly ICommitFacade _sut;

    public CommitFacadeTests()
    {
        _mediator = Substitute.For<IMediator>();
        _sut = new CommitFacade(_mediator);
    }

    [Fact]
    public async Task LoadBranchCommitsAsync_ShouldCallSend_WhenEver()
    {
        // Arrange
        const string repositoryName = "fakeRepoName";
        const string branchName = "fakeBranchName";
        const int page = 1;
        const int limit = 5;
        var queryDto = new LoadBranchCommitsQueryDto
        {
            RepositoryName = repositoryName,
            BranchName = branchName,
            Limit = limit,
            Page = page
        };

        // Act
        await _sut.LoadBranchCommitsAsync(queryDto, default);
        // Assert
        await _mediator.Send(Arg.Is<LoadBranchCommitsQuery>(x => x.RepositoryName == repositoryName
            && x.BranchName == branchName
            && x.Page == page
            && x.Limit == limit), default);
    }
    
}