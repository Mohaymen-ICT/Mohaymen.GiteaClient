using System;
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

internal sealed class LoadBranchCommitsQuery : IRequest<ApiResponse<LoadBranchCommitsResponseDto>>
{
    public required string RepositoryName { get; init; }
    public required string BranchName { get; init; }
    public int Page { get; init; }
    public int Limit { get; init; }
}

internal sealed class LoadBranchCommitsQueryHandler : IRequestHandler<LoadBranchCommitsQuery, ApiResponse<LoadBranchCommitsResponseDto>>
{
    private readonly IValidator<LoadBranchCommitsQuery> _validator;
    private readonly ICommitRestClient _commitRestClient;
    private readonly IOptions<GiteaApiConfiguration> _giteaOptions;

    public LoadBranchCommitsQueryHandler(IValidator<LoadBranchCommitsQuery> validator,
        ICommitRestClient commitRestClient,
        IOptions<GiteaApiConfiguration> giteaOptions)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _commitRestClient = commitRestClient ?? throw new ArgumentNullException(nameof(commitRestClient));
        _giteaOptions = giteaOptions ?? throw new ArgumentNullException(nameof(giteaOptions));
    }

    public async Task<ApiResponse<LoadBranchCommitsResponseDto>> Handle(LoadBranchCommitsQuery request, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(request);
        return await _commitRestClient.LoadBranchCommitsAsync(_giteaOptions.Value.RepositoriesOwner,
            request.RepositoryName,
            request.BranchName,
            request.Page,
            request.Limit);
    }
}