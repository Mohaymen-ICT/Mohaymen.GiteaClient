using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using Mohaymen.GiteaClient.Core.Configs;
using Mohaymen.GiteaClient.Gitea.PullRequest.Common.ApiCall.Abstractions;
using Mohaymen.GiteaClient.Gitea.PullRequest.Common.Enums;
using Mohaymen.GiteaClient.Gitea.PullRequest.MergePullRequest.Mappers;
using Refit;

namespace Mohaymen.GiteaClient.Gitea.PullRequest.MergePullRequest.Commands;

public record MergePullRequestCommand : IRequest<ApiResponse<Unit>> 
{
    public required string RepositoryName { get; init; }
    public required int Index { get; init; }
    public required MergeType MergeType { get; init; }
    public bool DeleteBranchAfterMerge { get; init; }
    public bool ForceMerge { get; init; }
    public bool MergeWhenChecksSucceed { get; init; }
    public string? MergeTitle { get; init; }
    public string? MergeMessage { get; init; }
}

internal class MergePullRequestCommandHandler : IRequestHandler<MergePullRequestCommand, ApiResponse<Unit>>
{
    private readonly IPullRequestRestClient _pullRequestRestClient;
    private readonly IOptions<GiteaApiConfiguration> _options;
    private readonly IValidator<MergePullRequestCommand> _validator;

    public MergePullRequestCommandHandler(IPullRequestRestClient pullRequestRestClient,
        IOptions<GiteaApiConfiguration> options,
        IValidator<MergePullRequestCommand> validator)
    {
        _pullRequestRestClient = pullRequestRestClient ?? throw new ArgumentNullException(nameof(pullRequestRestClient));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }
    
    public async Task<ApiResponse<Unit>> Handle(MergePullRequestCommand command, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(command);
        var mergePullRequestRequest = command.Map();
        var owner = _options.Value.RepositoriesOwner;
        return await _pullRequestRestClient.MergePullRequestAsync(owner, 
                command.RepositoryName, 
                command.Index, 
                mergePullRequestRequest, 
                cancellationToken)
            .ConfigureAwait(false);
    }
}