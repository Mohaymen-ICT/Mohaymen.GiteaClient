using FluentAssertions;
using FluentValidation;
using MediatR;
using Mohaymen.GitClient.APICall.Domain;
using Mohaymen.GitClient.APICall.Facades.Abstractions;
using Mohaymen.GitClient.Gitea.Business.Commands.Repository.CreateRepository;
using NSubstitute;
using Xunit;

namespace Mohaymen.GitClient.Tests.Gitea.Business.Commands.Repository.CreateRepository;

public class CreateRepositoryCommandHandlerTests
{
    private readonly IApiCallFacade _apiCallFacade;
    private readonly InlineValidator<CreateRepositoryCommand> _validator;
    private readonly IRequestHandler<CreateRepositoryCommand, GiteaResponseDto<CreateRepositoryResponseDto>> _sut;

    public CreateRepositoryCommandHandlerTests()
    {
        _apiCallFacade = Substitute.For<IApiCallFacade>();
        _validator = new InlineValidator<CreateRepositoryCommand>();
        _sut = new CreateRepositoryCommandHandler(_validator, _apiCallFacade);
    }

    [Fact]
    public async Task Handle_ShouldThrowsValidationExceptionAndNotCallSendAsync_WhenValidatorThrowsException()
    {
        // Arrange
        _validator.RuleFor(x => x.Name).NotEmpty();
        var command = new CreateRepositoryCommand()
        {
            Name = null,
            DefaultBranch = "main"
        };

        // Act
        var actual =  async () =>  await _sut.Handle(command, default);
        // Assert
        await actual.Should().ThrowAsync<ValidationException>();
        await _apiCallFacade
            .DidNotReceive()
            .SendAsync<CreateRepositoryCommand, CreateRepositoryResponseDto>(
                Arg.Any<HttpRestApiDto<CreateRepositoryCommand>>());

    }
    
    [Fact]
    public async Task Handle_ShouldCallSendAsync_WhenInputsAreProvided()
    {
        // Arrange
        const string repoName = "test_repo";
        const string defaultBranch = "main";
        var command = new CreateRepositoryCommand()
        {
            Name = repoName,
            DefaultBranch = defaultBranch
        };
        
        // Act
        await _sut.Handle(command, default);

        // Assert
        _apiCallFacade
            .Received(1)
            .SendAsync<CreateRepositoryCommand, CreateRepositoryResponseDto>(Arg.Is<HttpRestApiDto<CreateRepositoryCommand>>(x => x.BodyDto == command
                && x.HttpMethod == HttpMethod.Post)
                , default);
    }
}