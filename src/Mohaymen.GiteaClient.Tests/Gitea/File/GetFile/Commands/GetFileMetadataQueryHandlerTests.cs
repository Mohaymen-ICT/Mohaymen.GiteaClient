using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using Mohaymen.GiteaClient.Core.Configs;
using Mohaymen.GiteaClient.Gitea.File.Common.ApiCall.Abstractions;
using Mohaymen.GiteaClient.Gitea.File.Common.Encoding.Abstraction;
using Mohaymen.GiteaClient.Gitea.File.GetFile.Queries;
using Mohaymen.GiteaClient.Gitea.File.GetRepositoryFile.Context;
using Mohaymen.GiteaClient.Gitea.File.GetRepositoryFile.Dtos;
using NSubstitute;
using Refit;
using Xunit;

namespace Mohaymen.GiteaClient.Tests.Gitea.File.GetFile.Commands;

public class GetFileMetadataQueryHandlerTests
{
    private readonly IFileRestClient _fileRestClient;
    private readonly IOptions<GiteaApiConfiguration> _options;
    private readonly InlineValidator<GetFileMetadataQuery> _validator;
    private readonly IRequestHandler<GetFileMetadataQuery, ApiResponse<GetFileResponseDto>> _sut;
    private readonly IContentDecoder _contentDecoder;

    public GetFileMetadataQueryHandlerTests()
    {
        _fileRestClient = Substitute.For<IFileRestClient>();
        _options = Substitute.For<IOptions<GiteaApiConfiguration>>();
        _contentDecoder = Substitute.For<IContentDecoder>();
        _validator = new InlineValidator<GetFileMetadataQuery>();
        _sut = new GetFileMetadataQueryHandler(_fileRestClient, _contentDecoder, _options, _validator);
    }

    [Fact]
    public async Task Handle_ShouldThrowsValidationException_WhenInputIsInvalid()
    {
        // Arrange
        _validator.RuleFor(x => x).Must(_ => false);
        var command = new GetFileMetadataQuery
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
        var command = new GetFileMetadataQuery
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
        var mockedResponse = new ApiResponse<GetFileResponseDto>(Substitute.For<HttpResponseMessage>(),
            Substitute.For<GetFileResponseDto>(),
            null!);
        _fileRestClient.GetFileAsync(owner, 
                repositoryName, 
                filePath, 
                Arg.Any<GetFileRequest>(), 
                Arg.Any<CancellationToken>())
            .Returns(mockedResponse);

        // Act
        await _sut.Handle(command, default);
        
        // Assert
        await _fileRestClient.Received(1).GetFileAsync(owner, 
            repositoryName, 
            filePath, 
            Arg.Is<GetFileRequest>(x => 
            x.ReferenceName == referenceName),
            default);
    }
}