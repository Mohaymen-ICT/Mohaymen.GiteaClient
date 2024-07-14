using MediatR;
using Mohaymen.GiteaClient.Gitea.Branch.Common.Facade;
using Mohaymen.GiteaClient.Gitea.Branch.Common.Facade.Abstractions;
using Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Commands;
using Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Dtos;
using NSubstitute;
using Xunit;

namespace Mohaymen.GiteaClient.Tests.Gitea.Branch.Common.Facade;

public class BranchFacadeTests
{
    private readonly IMediator _mediator;
    private readonly IBranchFacade _sut;

    public BranchFacadeTests()
    {
        _mediator = Substitute.For<IMediator>();
        _sut = new BranchFacade(_mediator);
    }

    [Fact]
    public async Task CreateBranchAsync_ShouldCallSend_WhenEver()
    {
        // Arrange
        const string repositoryName = "repo";
        const string newBranchName = "new_branch";
        const string oldReferenceName = "old_ref";
        var commandDto = new CreateBranchCommandDto
        {
            RepositoryName = repositoryName,
            NewBranchName = newBranchName,
            OldReferenceName = oldReferenceName
        };

        // Act
        await _sut.CreateBranchAsync(commandDto, default);

        // Assert
        await _mediator.DidNotReceive().Send(Arg.Is<CreateBranchCommand>(x => x.RepositoryName == repositoryName
                                                                          && x.NewBranchName == newBranchName
                                                                          && x.OldReferenceName == oldReferenceName));
    }
}