using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using Mohaymen.GiteaClient.Core.Configs;
using Mohaymen.GiteaClient.Gitea.Repository.Common.ApiCall.Abstractions;
using Mohaymen.GiteaClient.Gitea.Repository.DeleteRepository.Commands;
using Mohaymen.GiteaClient.Gitea.Repository.DeleteRepository.Dto;
using NSubstitute;
using Refit;
using Xunit;

namespace Mohaymen.GiteaClient.Tests.Gitea.Repository.DeleteRepository.Commands;

public class DeleteRepositoryCommandHandlerTests
{
    private readonly InlineValidator<DeleteRepositoryCommand> _validator;
    private readonly IRepositoryRestClient _repositoryRestClient;
    private readonly IOptions<GiteaApiConfiguration> _giteaOptions;
    private readonly IRequestHandler<DeleteRepositoryCommand, ApiResponse<DeleteRepositoryResponseDto>> _sut;

    public DeleteRepositoryCommandHandlerTests()
    {
        _validator = new InlineValidator<DeleteRepositoryCommand>();
        _repositoryRestClient = Substitute.For<IRepositoryRestClient>();
        _giteaOptions = Substitute.For<IOptions<GiteaApiConfiguration>>();
        _sut = new DeleteRepositoryCommandHandler(_validator, _repositoryRestClient, _giteaOptions);
    }

    [Fact]
    public async Task Handle_ShouldThrowsValidationException_WhenInputIsNotValid()
    {
        // Arrange
        var deleteRepositoryCommand = new DeleteRepositoryCommand
        {
            RepositoryName = "fakeRepo"
        };
        _validator.RuleFor(x => x).Must(x => false);

        // Act
        var actual = async () => await _sut.Handle(deleteRepositoryCommand, default);

        // Assert
        await actual.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task Handle_ShouldCallDeleteRepositoryAsync_WhenInputIsProvidedProperly()
    {
        // Arrange
        const string repositoryName = "fakeRepo";
        const string repositoryOwner = "fakeOwner";
        var deleteRepositoryCommand = new DeleteRepositoryCommand
        {
            RepositoryName = repositoryName
        };
        var giteaConfig = new GiteaApiConfiguration
        {
            BaseUrl = "fakeUrl",
            PersonalAccessToken = "fakeToken",
            RepositoriesOwner = repositoryOwner
        };
        _giteaOptions.Value.Returns(giteaConfig);

        // Act
        await _sut.Handle(deleteRepositoryCommand, default);

        // Assert
        await _repositoryRestClient.Received(1).DeleteRepositoryAsync(repositoryOwner, repositoryName);
    }
    
}