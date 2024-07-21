using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using Mohaymen.GiteaClient.Core.Configs;
using Mohaymen.GiteaClient.Gitea.Commit.Common.ApiCall;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Dtos.Response;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Mappers;
using Refit;

namespace Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Commands;

internal class CreateCommitCommand : IRequest<ApiResponse<CreateCommitResponseDto>>
{
    public required string RepositoryName { get; init; }
    
    public required string BranchName { get; init; }
    
    public required string CommitMessage { get; init; }
    
    public required List<FileCommitCommandModel> FileCommitCommands { get; init; }
}

internal class CreateCommitCommandHandler : IRequestHandler<CreateCommitCommand, ApiResponse<CreateCommitResponseDto>>
{
    private readonly IValidator<CreateCommitCommand> _validator;
    private readonly ICommitRestClient _commitRestClient;
    private readonly IOptions<GiteaApiConfiguration> _giteaOptions;

    public CreateCommitCommandHandler(IValidator<CreateCommitCommand> validator,
        ICommitRestClient commitRestClient,
        IOptions<GiteaApiConfiguration> giteaOptions)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _commitRestClient = commitRestClient ?? throw new ArgumentNullException(nameof(commitRestClient));
        _giteaOptions = giteaOptions ?? throw new ArgumentNullException(nameof(giteaOptions));
    }

    public async Task<ApiResponse<CreateCommitResponseDto>> Handle(CreateCommitCommand createCommitCommand, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(createCommitCommand);
        var request = createCommitCommand.MapToRequest();
        return await _commitRestClient.CreateCommitAsync(_giteaOptions.Value.RepositoriesOwner,
            createCommitCommand.RepositoryName,
            request);
    }
}