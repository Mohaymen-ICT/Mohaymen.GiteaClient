using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using Mohaymen.GiteaClient.Core.Configs;
using Mohaymen.GiteaClient.Gitea.Commit.Common.ApiCall;
using Mohaymen.GiteaClient.Gitea.Commit.GetBranchCommits.Dtos;
using Refit;

namespace Mohaymen.GiteaClient.Gitea.Commit.GetBranchCommits.Queries;

internal sealed class GetSingleCommitQuery : IRequest<ApiResponse<GetSingleCommitResponseDto>>
{
    public required string RepositoryName { get; init; }
    public required string? CommitSha { get; init; }
}

internal sealed class
    GetSingleCommitQueryHandler : IRequestHandler<GetSingleCommitQuery, ApiResponse<GetSingleCommitResponseDto>>
{
    private readonly IValidator<GetSingleCommitQuery> _validator;
    private readonly ICommitRestClient _commitRestClient;
    private readonly IOptions<GiteaApiConfiguration> _giteaOptions;

    public GetSingleCommitQueryHandler(IValidator<GetSingleCommitQuery> validator,
        ICommitRestClient commitRestClient,
        IOptions<GiteaApiConfiguration> giteaOptions)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _commitRestClient = commitRestClient ?? throw new ArgumentNullException(nameof(commitRestClient));
        _giteaOptions = giteaOptions ?? throw new ArgumentNullException(nameof(giteaOptions));
    }

    public async Task<ApiResponse<GetSingleCommitResponseDto>> Handle(GetSingleCommitQuery request,
        CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(request);
        return await _commitRestClient.GetSingleCommitAsync(_giteaOptions.Value.RepositoriesOwner,
            request.RepositoryName,
            request.CommitSha);
    }
}