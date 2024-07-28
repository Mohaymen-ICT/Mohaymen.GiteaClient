using MediatR;
using Mohaymen.GiteaClient.Gitea.Commit.Common.Facades;
using Mohaymen.GiteaClient.Gitea.Commit.Common.Facades.Abstractions;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Commands;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Dtos.Request;
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
        await _mediator.Send(Arg.Is<LoadBranchCommitsQuery>(x => x.RepositoryName.Equals(repositoryName)
            && x.BranchName.Equals(branchName)
            && x.Page == page
            && x.Limit == limit), default);
    }

    [Fact]
    public async Task CreateCommitAsync_ShouldCallSend_WhenEver()
    {
        // Arrange
        const string repositoryName = "fakeRepoName";
        const string branchName = "fakeBranchName";
        const string commitMessage = "fakeMessage";
        const string fakePath1 = "path1";
        const string fakePath2 = "path2";
        const string fakePath3 = "path3";
        const string fakeContent1 = "content1";
        const string fakeContent2 = "content2";
        const string fakeContent3 = "content3";
        const CommitActionDto operation1 = CommitActionDto.Create;
        const CommitActionDto operation2 = CommitActionDto.Update;
        const CommitActionDto operation3 = CommitActionDto.Delete;
        var fileDtos = new List<FileCommitDto>()
        {
            new()
            {
                Path = fakePath1,
                Content = fakeContent1,
                CommitActionDto = operation1
            },
            new()
            {
                Path = fakePath2,
                Content = fakeContent2,
                CommitActionDto = operation2
            },
            new()
            {
                Path = fakePath3,
                Content = fakeContent3,
                CommitActionDto = operation3
            }
        };
        var commandDto = new CreateCommitCommandDto
        {
            RepositoryName = repositoryName,
            BranchName = branchName,
            CommitMessage = commitMessage,
            FileDtos = fileDtos
        };

        // Act
        await _sut.CreateCommitAsync(commandDto, default);

        // Assert
        await _mediator.Received(1).Send(Arg.Is<CreateCommitCommand>(x => x.RepositoryName == repositoryName
            && x.BranchName.Equals(branchName) 
            && x.CommitMessage.Equals(commitMessage)
            && x.FileCommitCommands.Count == 3
            && x.FileCommitCommands[0].Path.Equals(fakePath1)
            && x.FileCommitCommands[0].Content.Equals(fakeContent1)
            && x.FileCommitCommands[0].CommitActionCommand == CommitActionCommand.Create
            && x.FileCommitCommands[1].Path.Equals(fakePath2)
            && x.FileCommitCommands[1].Content.Equals(fakeContent2)
            && x.FileCommitCommands[1].CommitActionCommand == CommitActionCommand.Update
            && x.FileCommitCommands[2].Path.Equals(fakePath3) 
            && x.FileCommitCommands[2].Content.Equals(fakeContent3)
            && x.FileCommitCommands[2].CommitActionCommand == CommitActionCommand.Delete), 
            default);
    }
    
}