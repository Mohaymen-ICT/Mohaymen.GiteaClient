using MediatR;
using Mohaymen.GiteaClient.Gitea.Repository.Common.Facade;
using Mohaymen.GiteaClient.Gitea.Repository.Common.Facade.Abstractions;
using Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Commands;
using Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Dtos;
using Mohaymen.GiteaClient.Gitea.Repository.DeleteRepository.Commands;
using Mohaymen.GiteaClient.Gitea.Repository.DeleteRepository.Dto;
using Mohaymen.GiteaClient.Gitea.Repository.SearchRepository.Dtos;
using Mohaymen.GiteaClient.Gitea.Repository.SearchRepository.Queries;
using NSubstitute;
using Xunit;

namespace Mohaymen.GiteaClient.Tests.Gitea.Repository.Common.Facade;

public class RepositoryFacadeTests
{
    private readonly IMediator _mediator;
    private readonly IRepositoryFacade _sut;

    public RepositoryFacadeTests()
    {
        _mediator = Substitute.For<IMediator>();
        _sut = new RepositoryFacade(_mediator);
    }

    [Fact]
    public async Task CreateRepositoryAsync_ShouldCallSend_WhenEver()
    {
        // Arrange
        const string repositoryName = "test_repo";
        const string branchName = "main";
        var commandDto = new CreateRepositoryCommandDto
        {
            Name = repositoryName,
            DefaultBranch = branchName,
            IsPrivateBranch = true
        };
        
        // Act
        await _sut.CreateRepositoryAsync(commandDto, default);

        // Assert
        await _mediator.Received(1).Send(Arg.Is<CreateRepositoryCommand>(x => x.DefaultBranch == branchName &&
                                                                              x.Name == repositoryName &&
                                                                              x.IsPrivateBranch == true), default);
    }

    [Fact]
    public async Task SearchRepositoryAsync_ShouldCallSend_WhenEver()
    {
        // Arrange
        const string query = "fakeQuery";
        var searchRepositoryQueryDto = new SearchRepositoryQueryDto
        {
            Query = query
        };

        // Act
        await _sut.SearchRepositoryAsync(searchRepositoryQueryDto, default);

        // Assert
        await _mediator.Received(1).Send(Arg.Is<SearchRepositoryQuery>(x => x.Query == query));
    }

    [Fact]
    public async Task DeleteRepositoryAsync_ShouldCallSend_WhenEver()
    {
        // Arrange
        const string repositoryName = "fakeRepoName";
        var deleteRepositoryCommandDto = new DeleteRepositoryCommandDto
        {
            RepositoryName = repositoryName
        };

        // Act
        await _sut.DeleteRepositoryAsync(deleteRepositoryCommandDto, default);

        // Assert
        await _mediator.Received(1).Send(Arg.Is<DeleteRepositoryCommand>(x => x.RepositoryName == repositoryName), default);
    }
}