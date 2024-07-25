using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using Mohaymen.GiteaClient.Core.Configs;
using Mohaymen.GiteaClient.Gitea.File.Common.ApiCall.Abstractions;
using Mohaymen.GiteaClient.Gitea.File.GetFile.Commands;
using Mohaymen.GiteaClient.Gitea.File.GetRepositoryFile.Context;
using Mohaymen.GiteaClient.Gitea.File.GetRepositoryFile.Dtos;
using NSubstitute;
using Refit;
using Xunit;

namespace Mohaymen.GiteaClient.Tests.Gitea.File.GetFile.Commands;

public class GetFileCommandHandlerTests
{
    private readonly IFileRestClient _fileRestClient;
    private readonly IOptions<GiteaApiConfiguration> _options;
    private readonly InlineValidator<GetFileCommand> _validator;
    private readonly IRequestHandler<GetFileCommand, ApiResponse<GetFileResponseDto>> _sut;

    public GetFileCommandHandlerTests()
    {
        _fileRestClient = Substitute.For<IFileRestClient>();
        _options = Substitute.For<IOptions<GiteaApiConfiguration>>();
        _validator = new InlineValidator<GetFileCommand>();
        _sut = new GetFileCommandHandler(_fileRestClient, _options, _validator);
    }

    [Fact]
    public async Task Handle_ShouldThrowsValidationException_WhenInputIsInvalid()
    {
        // Arrange
        _validator.RuleFor(x => x).Must(_ => false);
        var command = new GetFileCommand
        {
            RepositoryName = "repo",
            FilePath = "path"
        };

        // Act
        var actual = async () => await _sut.Handle(command, default);

        // Assert
        await actual.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task Handle_ShouldCallGetFileAsync_AndInputsAreValid()
    {
        // Arrange
        _validator.RuleFor(x => x).Must(_ => true);
        const string owner = "owner";
        const string repositoryName = "repo";
        const string filePath = "file_path";
        const string referenceName = "ref";
        var command = new GetFileCommand
        {
            RepositoryName = repositoryName,
            FilePath = filePath,
            ReferenceName = referenceName
        };
        _options.Value.Returns(new GiteaApiConfiguration
        {
            BaseUrl = "url",
            PersonalAccessToken = "token",
            RepositoriesOwner = owner
        });

        // Act
        await _sut.Handle(command, default);
        
        // Assert
        await _fileRestClient.Received(1).GetFileAsync(owner, 
            repositoryName, 
            filePath, 
            Arg.Is<GetFileRequest>(x => 
            x.ReferenceName == referenceName));
    }
}