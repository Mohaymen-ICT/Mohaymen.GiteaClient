using MediatR;
using Mohaymen.GitClient.Gitea.Business.Commands.Repository.CreateRepository;
using Mohaymen.GitClient.Gitea.Facades;
using Mohaymen.GitClient.Gitea.Facades.Abstractions;
using NSubstitute;
using Xunit;

namespace Mohaymen.GitClient.Tests.Gitea.Facades;

public class GiteaFacadeTests
{
    private readonly IMediator _mediator;
    private readonly IGiteaFacade _sut;

    public GiteaFacadeTests()
    {
        _mediator = Substitute.For<IMediator>();
        _sut = new GiteaFacade(_mediator);
    }

    [Fact]
    public async Task CreateRepository_ShouldCallSend_WhenInputsAreProvided()
    {
        // Arrange
        var command = new CreateRepositoryCommand
        {
            Name = "Test",
            DefaultBranch = "Main",
            IsPrivateBranch = true
        };

        // Act
        var actual = await _sut.CreateRepository(command, default);

        // Assert
        _mediator.Received(1).Send(command, default);
    }
}