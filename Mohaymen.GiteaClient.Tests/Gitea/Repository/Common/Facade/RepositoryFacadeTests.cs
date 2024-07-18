using MediatR;
using Mohaymen.GiteaClient.Gitea.Repository.Common.Facade;
using Mohaymen.GiteaClient.Gitea.Repository.Common.Facade.Abstractions;
using Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Commands;
using Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Dtos;
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
    
}