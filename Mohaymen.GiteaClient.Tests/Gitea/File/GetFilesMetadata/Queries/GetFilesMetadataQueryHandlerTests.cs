using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using Mohaymen.GiteaClient.Core.Configs;
using Mohaymen.GiteaClient.Gitea.File.Common.ApiCall.Abstractions;
using Mohaymen.GiteaClient.Gitea.File.GetFilesMetadata.Queries;
using Mohaymen.GiteaClient.Gitea.File.GetRepositoryFile.Dtos;
using NSubstitute;
using Refit;
using Xunit;

namespace Mohaymen.GiteaClient.Tests.Gitea.File.GetFilesMetadata.Queries;

public class GetFilesMetadataQueryHandlerTests
{
    private readonly InlineValidator<GetFilesMetadataQuery> _validator;
    private readonly IFileRestClient _fileRestClient;
    private readonly IOptions<GiteaApiConfiguration> _giteaOptions;
    private readonly IRequestHandler<GetFilesMetadataQuery, ApiResponse<List<GetFileResponseDto>>> _sut;

    public GetFilesMetadataQueryHandlerTests()
    {
        _validator = new InlineValidator<GetFilesMetadataQuery>();
        _fileRestClient = Substitute.For<IFileRestClient>();
        _giteaOptions = Substitute.For<IOptions<GiteaApiConfiguration>>();
        _sut = new GetFilesMetadataQueryHandler(_validator, _fileRestClient, _giteaOptions);
    }

    [Fact]
    public async Task Handle_ShouldThrowsValidationException_WhenInputIsNotProvidedProperly()
    {
        // Arrange
        var request = new GetFilesMetadataQuery
        {
            RepositoryName = "fakeRepo",
            BranchName = "fakeBranch"
        };
        _validator.RuleFor(x => x).Must(x => false);

        // Act
        var actual = async () => await _sut.Handle(request, default);

        // Assert
        await actual.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task Handle_ShouldCallGetFilesMetadataAsync_WhenInputsAreProvidedProperly()
    {
        // Arrange
        const string repositoryName = "fakeRepo";
        const string branchName = "fakeBranch";
        var request = new GetFilesMetadataQuery
        {
            RepositoryName = repositoryName,
            BranchName = branchName
        };
        const string repositoryOwner = "fakeOwner";
        var giteaApiConfiguration = new GiteaApiConfiguration
        {
            BaseUrl = "fakeUrl",
            PersonalAccessToken = "token",
            RepositoriesOwner = repositoryOwner
        };
        _giteaOptions.Value.Returns(giteaApiConfiguration);
        
        // Act
        await _sut.Handle(request, default);
        // Assert
        await _fileRestClient.Received(1).GetFilesMetadataAsync(repositoryOwner,
            repositoryName,
            branchName,
            default);
    }
}