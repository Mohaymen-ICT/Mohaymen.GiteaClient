using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using Mohaymen.GiteaClient.Core.Configs;
using Mohaymen.GiteaClient.Gitea.Commit.Common.ApiCall;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Dtos.Response;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Mappers;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Services.Base64Encoder.Abstractions;
using Refit;

namespace Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Commands;

internal sealed record CreateCommitCommand : IRequest<ApiResponse<CreateCommitResponseDto>>
{
    public required string RepositoryName { get; init; }
    
    public required string BranchName { get; init; }
    
    public required string CommitMessage { get; init; }
    
    public required List<FileCommitCommandModel> FileCommitCommands { get; init; }
}

internal sealed class CreateCommitCommandHandler : IRequestHandler<CreateCommitCommand, ApiResponse<CreateCommitResponseDto>>
{
    private readonly IValidator<CreateCommitCommand> _validator;
    private readonly ICommitRestClient _commitRestClient;
    private readonly IOptions<GiteaApiConfiguration> _giteaOptions;
    private readonly IBase64CommitEncoder _base64CommitEncoder;

    public CreateCommitCommandHandler(IValidator<CreateCommitCommand> validator,
        ICommitRestClient commitRestClient,
        IOptions<GiteaApiConfiguration> giteaOptions,
        IBase64CommitEncoder base64CommitEncoder)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _commitRestClient = commitRestClient ?? throw new ArgumentNullException(nameof(commitRestClient));
        _giteaOptions = giteaOptions ?? throw new ArgumentNullException(nameof(giteaOptions));
        _base64CommitEncoder = base64CommitEncoder ?? throw new ArgumentNullException(nameof(base64CommitEncoder));
    }

    public async Task<ApiResponse<CreateCommitResponseDto>> Handle(CreateCommitCommand createCommitCommand, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(createCommitCommand);
        var command = createCommitCommand with
        {
            FileCommitCommands = _base64CommitEncoder.EncodeFileContentsToBase64(createCommitCommand.FileCommitCommands).ToList()
        };
        var request = command.MapToRequest();
        return await _commitRestClient.CreateCommitAsync(_giteaOptions.Value.RepositoriesOwner,
            createCommitCommand.RepositoryName,
            request);
    }
}