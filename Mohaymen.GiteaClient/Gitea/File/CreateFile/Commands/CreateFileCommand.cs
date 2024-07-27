using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using Mohaymen.GiteaClient.Core.Configs;
using Mohaymen.GiteaClient.Gitea.File.Common.ApiCall.Abstractions;
using Mohaymen.GiteaClient.Gitea.File.Common.Encoding.Abstraction;
using Mohaymen.GiteaClient.Gitea.File.CreateFile.Dtos;
using Mohaymen.GiteaClient.Gitea.File.CreateFile.Mappers;
using Mohaymen.GiteaClient.Gitea.File.CreateFile.Models;
using Refit;

namespace Mohaymen.GiteaClient.Gitea.File.CreateFile.Commands;

public class CreateFileCommand : IRequest<ApiResponse<CreateFileResponseDto>>
{
    public required string RepositoryName { get; init; }
    public required string FilePath { get; init; }
    public required string Content { get; init; }
    public Identity? Author { get; init; }
    public string? BranchName { get; init; }
    public string? CommitMessage { get; init; }
}

internal class CreateFileCommandHandler : IRequestHandler<CreateFileCommand, ApiResponse<CreateFileResponseDto>>
{
    private readonly IFileRestClient _fileRestClient;
    private readonly IContentEncoder _contentEncoder;
    private readonly IOptions<GiteaApiConfiguration> _options;
    private readonly IValidator<CreateFileCommand> _validator;

    public CreateFileCommandHandler(IFileRestClient fileRestClient,
        IContentEncoder contentEncoder,
        IOptions<GiteaApiConfiguration> options,
        IValidator<CreateFileCommand> validator)
    {
        _fileRestClient = fileRestClient ?? throw new ArgumentNullException(nameof(fileRestClient));
        _contentEncoder = contentEncoder ?? throw new ArgumentNullException(nameof(contentEncoder));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }
    
    public async Task<ApiResponse<CreateFileResponseDto>> Handle(CreateFileCommand command, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(command);
        var encodedContent = _contentEncoder.Base64Encode(command.Content);
        var createFileRequest = command.ToCreateFileRequest(encodedContent);
        var owner = _options.Value.RepositoriesOwner;
        return await _fileRestClient.CreateFileAsync(owner, command.RepositoryName, command.FilePath, createFileRequest, cancellationToken)
            .ConfigureAwait(false);
    }
}