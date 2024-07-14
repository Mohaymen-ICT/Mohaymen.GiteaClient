using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Mohaymen.GiteaClient.Gitea.Branch.Common.ApiCall.Abstractions;
using Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Dtos;
using Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Mappers;
using Refit;

namespace Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Commands;

internal class CreateBranchCommand : IRequest<ApiResponse<CreateBranchResponseDto>>
{
    public string Owner { get; init; }
    public string RepositoryName { get; init; }
    public string NewBranchName { get; init; }
    public string OldReferenceName { get; init; }
}

internal class CreateBranchCommandHandler : IRequestHandler<CreateBranchCommand, ApiResponse<CreateBranchResponseDto>>
{
    private readonly IBranchRestClient _branchRestClient;
    private readonly IValidator<CreateBranchCommand> _validator;

    public CreateBranchCommandHandler(IBranchRestClient branchRestClient,
        IValidator<CreateBranchCommand> validator)
    {
        _branchRestClient = branchRestClient ?? throw new ArgumentNullException(nameof(branchRestClient));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public async Task<ApiResponse<CreateBranchResponseDto>> Handle(CreateBranchCommand command, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(command);
        var createBranchRequest = CreateBranchRequestMapper.Map(command);
        return await _branchRestClient.CreateBranchAsync(command.Owner, command.RepositoryName, createBranchRequest)
            .ConfigureAwait(false);
    }
}

