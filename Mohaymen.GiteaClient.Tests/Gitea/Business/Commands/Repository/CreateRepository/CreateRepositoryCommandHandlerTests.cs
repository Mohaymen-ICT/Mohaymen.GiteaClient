﻿using MediatR;
using Mohaymen.GiteaClient.APICall.Domain;
using Mohaymen.GiteaClient.APICall.Facades.Abstractions;
using Mohaymen.GiteaClient.Gitea.Business.Commands.Repository.CreateRepository;
using Mohaymen.GiteaClient.Gitea.Domain.Dtos.Repository.CreateRepository;
using NSubstitute;
using Xunit;

namespace Mohaymen.GitClient.Tests.Gitea.Business.Commands.Repository.CreateRepository;

public class CreateRepositoryCommandHandlerTests
{
    private readonly IApiCallFacade _apiCallFacade;
    private readonly IRequestHandler<CreateRepositoryCommand, GiteaResponseDto<CreateRepositoryResponseDto>> _sut;

    public CreateRepositoryCommandHandlerTests()
    {
        _apiCallFacade = Substitute.For<IApiCallFacade>();
        _sut = new CreateRepositoryCommandHandler(_apiCallFacade);
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