using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using Mohaymen.GiteaClient.Core.Configs;
using Mohaymen.GiteaClient.Gitea.Branch.Common.ApiCall.Abstractions;
using Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Dtos;
using Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Mappers;
using Refit;

namespace Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Commands;

internal class CreateBranchCommand : IRequest<ApiResponse<BranchResponseDto>>
{
    public required string RepositoryName { get; init; }
    public required string NewBranchName { get; init; }
    public required string OldReferenceName { get; init; }
}

internal class CreateBranchCommandHandler : IRequestHandler<CreateBranchCommand, ApiResponse<BranchResponseDto>>
{
    private readonly IBranchRestClient _branchRestClient;
    private readonly IOptions<GiteaApiConfiguration> _options;
    private readonly IValidator<CreateBranchCommand> _validator;

    public CreateBranchCommandHandler(IBranchRestClient branchRestClient,
        IOptions<GiteaApiConfiguration> options,
        IValidator<CreateBranchCommand> validator)
    {
        _branchRestClient = branchRestClient ?? throw new ArgumentNullException(nameof(branchRestClient));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public async Task<ApiResponse<BranchResponseDto>> Handle(CreateBranchCommand command, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(command);
        var createBranchRequest = command.ToCreateBranchRequest();
        var owner = _options.Value.RepositoriesOwner;
        return await _branchRestClient.CreateBranchAsync(owner, command.RepositoryName, createBranchRequest)
            .ConfigureAwait(false);
    }
}

