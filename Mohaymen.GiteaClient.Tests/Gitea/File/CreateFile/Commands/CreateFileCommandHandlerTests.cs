﻿using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using Mohaymen.GiteaClient.Core.Configs;
using Mohaymen.GiteaClient.Gitea.File.Common.ApiCall.Abstractions;
using Mohaymen.GiteaClient.Gitea.File.Common.Encoding.Abstraction;
using Mohaymen.GiteaClient.Gitea.File.CreateFile.Commands;
using Mohaymen.GiteaClient.Gitea.File.CreateFile.Context;
using Mohaymen.GiteaClient.Gitea.File.CreateFile.Dtos;
using Mohaymen.GiteaClient.Gitea.File.CreateFile.Models;
using NSubstitute;
using Refit;
using Xunit;

namespace Mohaymen.GiteaClient.Tests.Gitea.File.CreateFile.Commands;

public class CreateFileCommandHandlerTests
{
    private readonly IFileRestClient _fileRestClient;
    private readonly IOptions<GiteaApiConfiguration> _options;
    private readonly InlineValidator<CreateFileCommand> _validator;
    private readonly IRequestHandler<CreateFileCommand, ApiResponse<CreateFileResponseDto>> _sut;
    private readonly IContentEncoder _contentEncoder;

    public CreateFileCommandHandlerTests()
    {
        _fileRestClient = Substitute.For<IFileRestClient>();
        _options = Substitute.For<IOptions<GiteaApiConfiguration>>();
        _contentEncoder = Substitute.For<IContentEncoder>();
        _validator = new InlineValidator<CreateFileCommand>();
        _sut = new CreateFileCommandHandler(_fileRestClient, _contentEncoder, _options, _validator);
    }

    [Fact]
    public async Task Handle_ShouldThrowsValidationException_WhenInputIsInvalid()
    {
        // Arrange
        _validator.RuleFor(x => x).Must(_ => false);
        var command = new CreateFileCommand
        {
            RepositoryName = "repo",
            FilePath = "path",
            Content = "content"
        };

        // Act
        var actual = async () => await _sut.Handle(command, default);

        // Assert
        await actual.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task Handle_ShouldCallCreateFileAsync_WhenInputsAreValid()
    {
        // Arrange
        _validator.RuleFor(x => x).Must(_ => true);
        const string owner = "owner";
        const string repositoryName = "repo";
        const string filePath = "file_path";
        const string content = "content";
        const string encodedContent = "encoded_content";
        const string branchName = "branch";
        const string commitMessage = "commit";
        var identity = new Identity
        {
            Email = "email",
            Name = "name"
        };
        var command = new CreateFileCommand
        {
            RepositoryName = repositoryName,
            FilePath = filePath,
            Content = content,
            Author = identity,
            BranchName = branchName,
            CommitMessage = commitMessage
        };
        _options.Value.Returns(new GiteaApiConfiguration
        {
            BaseUrl = "url",
            PersonalAccessToken = "token",
            RepositoriesOwner = owner
        });
        _contentEncoder.Base64Encode(content).Returns(encodedContent);

        var expected = new CreateFileRequest
        {
            Content = encodedContent,
            Author = identity,
            BranchName = branchName,
            CommitMessage = commitMessage
        };

        // Act
        await _sut.Handle(command, default);
        
        // Assert
        await _fileRestClient.Received(1).CreateFileAsync(owner, 
            repositoryName, 
            filePath, 
            Arg.Is<CreateFileRequest>(x => x.Equals(expected)),
            default);
    }
}