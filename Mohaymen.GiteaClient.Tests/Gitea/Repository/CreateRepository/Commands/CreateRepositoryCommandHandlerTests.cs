using FluentAssertions;
using FluentValidation;
using MediatR;
using Mohaymen.GiteaClient.Gitea.Repository.Common.ApiCall.Abstractions;
using Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Commands;
using Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Context;
using Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Dtos;
using NSubstitute;
using Refit;
using Xunit;

namespace Mohaymen.GiteaClient.Tests.Gitea.Repository.CreateRepository.Commands;

public class CreateRepositoryCommandHandlerTests
{
    private readonly IRepositoryRestClient _repositoryRestClient;
    private readonly InlineValidator<CreateRepositoryCommand> _validator;
    private readonly IRequestHandler<CreateRepositoryCommand, ApiResponse<CreateRepositoryResponseDto>> _sut;

    public CreateRepositoryCommandHandlerTests()
    {
        _repositoryRestClient = Substitute.For<IRepositoryRestClient>();
        _validator = new InlineValidator<CreateRepositoryCommand>();
        _sut = new CreateRepositoryCommandHandler(_repositoryRestClient, _validator);
    }

    [Fact]
    public async Task Handle_ShouldThrowsValidationException_WhenInputIsInvalidAndValidatorThrowsException()
    {
        // Arrange
        _validator.RuleFor(x => x).Must(x => false);
        var command = new CreateRepositoryCommand
        {
            Name = "test",
            IsPrivateBranch = true,
            DefaultBranch = "main"
        };
        
        // Act
        var actual = async () => await _sut.Handle(command, default);
        // Assert
        actual.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task Handle_ShouldCallCreateRepositoryAsync_WhenValidatorNotThrowsExceptionAndInputsAreValid()
    {
        // Arrange
        const string repositoryName = "test_repo";
        const string branchName = "main";
        var command = new CreateRepositoryCommand()
        {
            Name = repositoryName,
            DefaultBranch = branchName,
            IsPrivateBranch = true
        };

        // Act
        await _sut.Handle(command, default);
        // Assert
        await _repositoryRestClient.Received(1).CreateRepositoryAsync(Arg.Is<CreateRepositoryRequest>(x => x.DefaultBranch == branchName
         && x.Name == repositoryName
         && x.IsPrivateBranch == true));
    }
    
}