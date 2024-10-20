using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using Mohaymen.GiteaClient.Core.Configs;
using Mohaymen.GiteaClient.Gitea.Branch.Common.ApiCall.Abstractions;
using Mohaymen.GiteaClient.Gitea.Branch.Common.Dtos;
using Refit;

namespace Mohaymen.GiteaClient.Gitea.Branch.GetBranchList.Commands;

internal class GetBranchListCommand : IRequest<ApiResponse<List<BranchResponseDto>>>
{
    public required string RepositoryName { get; init; }
}

internal class GetBranchListCommandHandler : IRequestHandler<GetBranchListCommand, ApiResponse<List<BranchResponseDto>>>
{
    private readonly IBranchRestClient _branchRestClient;
    private readonly IOptions<GiteaApiConfiguration> _options;
    private readonly IValidator<GetBranchListCommand> _validator;

    public GetBranchListCommandHandler(IBranchRestClient branchRestClient,
        IOptions<GiteaApiConfiguration> options,
        IValidator<GetBranchListCommand> validator)
    {
        _branchRestClient = branchRestClient ?? throw new ArgumentNullException(nameof(branchRestClient));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }
    
    public async Task<ApiResponse<List<BranchResponseDto>>> Handle(GetBranchListCommand command, 
        CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(command);
        var owner = _options.Value.RepositoriesOwner;
        return await _branchRestClient.GetBranchListAsync(owner, command.RepositoryName).ConfigureAwait(false);
    }
}