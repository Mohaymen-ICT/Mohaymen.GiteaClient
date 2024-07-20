using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using Mohaymen.GiteaClient.Core.Configs;
using Mohaymen.GiteaClient.Gitea.PullRequest.Common.ApiCall.Abstractions;
using Mohaymen.GiteaClient.Gitea.PullRequest.CreatePullRequest.Dtos;
using Mohaymen.GiteaClient.Gitea.PullRequest.CreatePullRequest.Mappers;
using Refit;

namespace Mohaymen.GiteaClient.Gitea.PullRequest.CreatePullRequest.Commands;

internal class CreatePullRequestCommand : IRequest<ApiResponse<CreatePullRequestResponseDto>>
{
    public required string RepositoryName { get; init; } 
    public string? HeadBranch { get; init; }
    public string? BaseBranch { get; init; }
    public string? Body { get; init; }
    public string? Title { get; init; }
    public string? Assignee { get; init; }
    public List<string>? Assignees { get; init; } //TODO list or array ?
}

internal class CreatePullRequestCommandHandler : IRequestHandler<CreatePullRequestCommand, ApiResponse<CreatePullRequestResponseDto>>
{
    private readonly IPullRequestRestClient _pullRequestRestClient;
    private readonly IOptions<GiteaApiConfiguration> _options;
    private readonly IValidator<CreatePullRequestCommand> _validator;

    public CreatePullRequestCommandHandler(IPullRequestRestClient pullRequestRestClient,
        IOptions<GiteaApiConfiguration> options,
        IValidator<CreatePullRequestCommand> validator)
    {
        _pullRequestRestClient = pullRequestRestClient ?? throw new ArgumentNullException(nameof(pullRequestRestClient));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public async Task<ApiResponse<CreatePullRequestResponseDto>> Handle(CreatePullRequestCommand command, 
        CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(command);
        var createPullRequestRequest = command.ToCreatePullRequestRequest();
        var owner = _options.Value.RepositoriesOwner;
        return await _pullRequestRestClient.CreatePullRequestAsync(owner, command.RepositoryName, createPullRequestRequest)
            .ConfigureAwait(false);
    }
}