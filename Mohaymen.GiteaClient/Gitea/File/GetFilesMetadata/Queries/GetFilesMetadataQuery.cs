using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using Mohaymen.GiteaClient.Core.Configs;
using Mohaymen.GiteaClient.Gitea.File.Common.ApiCall.Abstractions;
using Mohaymen.GiteaClient.Gitea.File.GetRepositoryFile.Dtos;
using Refit;

namespace Mohaymen.GiteaClient.Gitea.File.GetFilesMetadata.Queries;

internal class GetFilesMetadataQuery : IRequest<ApiResponse<List<GetFileResponseDto>>>
{
    public required string RepositoryName { get; init; }
    public required string BranchName { get; init; }
}

internal class GetFilesMetadataQueryHandler : IRequestHandler<GetFilesMetadataQuery, ApiResponse<List<GetFileResponseDto>>>
{
    private readonly IValidator<GetFilesMetadataQuery> _validator;
    private readonly IFileRestClient _fileRestClient;
    private readonly IOptions<GiteaApiConfiguration> _giteaOptions;

    public GetFilesMetadataQueryHandler(IValidator<GetFilesMetadataQuery> validator,
        IFileRestClient fileRestClient,
        IOptions<GiteaApiConfiguration> giteaOptions)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _fileRestClient = fileRestClient ?? throw new ArgumentNullException(nameof(fileRestClient));
        _giteaOptions = giteaOptions ?? throw new ArgumentNullException(nameof(giteaOptions));
    }

    public async Task<ApiResponse<List<GetFileResponseDto>>> Handle(GetFilesMetadataQuery request, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(request);
        var repositoryOwner = _giteaOptions.Value.RepositoriesOwner;
        return await _fileRestClient.GetFilesMetadataAsync(repositoryOwner,
            request.RepositoryName,
            request.BranchName,
            cancellationToken);
    }
}