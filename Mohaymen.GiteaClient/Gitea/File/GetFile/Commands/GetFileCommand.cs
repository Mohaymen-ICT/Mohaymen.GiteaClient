using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using Mohaymen.GiteaClient.Core.Configs;
using Mohaymen.GiteaClient.Gitea.File.Common.ApiCall.Abstractions;
using Mohaymen.GiteaClient.Gitea.File.Common.Encoding.Abstraction;
using Mohaymen.GiteaClient.Gitea.File.GetFile.Mappers;
using Mohaymen.GiteaClient.Gitea.File.GetRepositoryFile.Dtos;
using Refit;

namespace Mohaymen.GiteaClient.Gitea.File.GetFile.Commands;

public class GetFileCommand : IRequest<ApiResponse<GetFileResponseDto>>
{
    public required string RepositoryName { get; init; }
    public required string FilePath { get; init; }
    public string? ReferenceName { get; init; }
}

internal class GetFileCommandHandler : IRequestHandler<GetFileCommand, ApiResponse<GetFileResponseDto>>
{
    private readonly IFileRestClient _fileRestClient;
    private readonly IContentDecoder _contentDecoder;
    private readonly IOptions<GiteaApiConfiguration> _options;
    private readonly IValidator<GetFileCommand> _validator;

    public GetFileCommandHandler(IFileRestClient fileRestClient,
        IContentDecoder contentDecoder,
        IOptions<GiteaApiConfiguration> options,
        IValidator<GetFileCommand> validator)
    {
        _fileRestClient = fileRestClient ?? throw new ArgumentNullException(nameof(fileRestClient));
        _contentDecoder = contentDecoder ?? throw new ArgumentNullException(nameof(contentDecoder));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public async Task<ApiResponse<GetFileResponseDto>> Handle(GetFileCommand command, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(command);
        var getFileRequest = command.ToGetFileRequest();
        var owner = _options.Value.RepositoriesOwner;
        var apiResponse = await _fileRestClient.GetFileAsync(owner, command.RepositoryName, command.FilePath, getFileRequest, cancellationToken)
            .ConfigureAwait(false);
        if (apiResponse?.Content != null)
        {
            apiResponse.Content.Content = _contentDecoder.Base64Decode(apiResponse.Content.Content);
        }

        return apiResponse;
    }
}

