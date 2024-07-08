using FluentAssertions;
using FluentValidation;
using MediatR;
using Mohaymen.GiteaClient.Gitea.Business.Commands.Repository.CreateRepository;
using Mohaymen.GiteaClient.Gitea.Domain.Dtos.Repository.CreateRepository;
using Mohaymen.GiteaClient.Gitea.Facades;
using Mohaymen.GiteaClient.Gitea.Facades.Abstractions;
using NSubstitute;
using Xunit;

namespace Mohaymen.GitClient.Tests.Gitea.Facades;

public class GiteaFacadeTests
{
    private readonly IMediator _mediator;
    private readonly InlineValidator<CreateRepositoryCommandDto> _createRepositoryModelValidator;
    private readonly IGiteaFacade _sut;

    public GiteaFacadeTests()
    {
        _mediator = Substitute.For<IMediator>();
        _createRepositoryModelValidator = new InlineValidator<CreateRepositoryCommandDto>();
        _sut = new GiteaFacade(_mediator, _createRepositoryModelValidator);
    }

    [Fact]
    public async Task CreateRepository_ShouldThrowValidationExceptionAndNotCallSend_WhenInputCommandDtoIsInvalid()
    {
        // Arrange
        var commandDto = new CreateRepositoryCommandDto
        {
            Name = "InvalidName",
            DefaultBranch = "Main",
            IsPrivateBranch = false
        };
        _createRepositoryModelValidator.RuleFor(x => x.Name).NotEqual("InvalidName");

        // Act
        var actual = async () => await _sut.CreateRepository(commandDto, default);

        // Assert
        await actual.Should().ThrowAsync<ValidationException>();
        await _mediator.DidNotReceive().Send(Arg.Any<CreateRepositoryCommand>(), default);
    }

    [Fact]
    public async Task CreateRepository_ShouldCallSend_WhenInputsAreProvided()
    {
        // Arrange
        const string repositoryName = "Test";
        const string branchName = "Main";
        var commandDto = new CreateRepositoryCommandDto
        {
            Name = repositoryName,
            DefaultBranch = branchName,
            IsPrivateBranch = true
        };

        // Act
        await _sut.CreateRepository(commandDto, default);

        // Assert
        await _mediator.Received(1).Send(Arg.Is<CreateRepositoryCommand>(x => x.Name == repositoryName
            && x.IsPrivateBranch == true
            && x.DefaultBranch == branchName),
            default);
    }
}