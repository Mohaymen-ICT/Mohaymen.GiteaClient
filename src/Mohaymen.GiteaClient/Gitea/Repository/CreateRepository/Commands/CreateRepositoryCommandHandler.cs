using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Mohaymen.GiteaClient.Gitea.Repository.Common.ApiCall.Abstractions;
using Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Dtos;
using Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Mappers;
using Refit;

namespace Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Commands;

internal class CreateRepositoryCommand : IRequest<ApiResponse<CreateRepositoryResponseDto>>
{
    public required string DefaultBranch { get; init; }

    public required string Name { get; init; }

    public bool IsPrivateBranch { get; init; }
    
    public bool AutoInit { get; init; }
}

internal class CreateRepositoryCommandHandler : IRequestHandler<CreateRepositoryCommand, ApiResponse<CreateRepositoryResponseDto>>
{
    private readonly IRepositoryRestClient _repositoryRestClient;
    private readonly IValidator<CreateRepositoryCommand> _validator;

    public CreateRepositoryCommandHandler(IRepositoryRestClient repositoryRestClient,
        IValidator<CreateRepositoryCommand> validator)
    {
        _repositoryRestClient = repositoryRestClient ?? throw new ArgumentNullException(nameof(repositoryRestClient));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public async Task<ApiResponse<CreateRepositoryResponseDto>> Handle(CreateRepositoryCommand command, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(command);
        var createRepositoryRequest = CreateRepositoryRequestMapper.Map(command);
        return await _repositoryRestClient.CreateRepositoryAsync(createRepositoryRequest).ConfigureAwait(false);
    }
}