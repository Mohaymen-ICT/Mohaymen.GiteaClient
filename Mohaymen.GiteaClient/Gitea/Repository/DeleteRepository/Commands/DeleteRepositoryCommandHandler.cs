using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using Mohaymen.GiteaClient.Core.Configs;
using Mohaymen.GiteaClient.Gitea.Repository.Common.ApiCall.Abstractions;
using Mohaymen.GiteaClient.Gitea.Repository.DeleteRepository.Dto;
using Refit;

namespace Mohaymen.GiteaClient.Gitea.Repository.DeleteRepository.Commands;

internal class DeleteRepositoryCommand : IRequest<ApiResponse<DeleteRepositoryResponseDto>>
{
    public required string RepositoryName { get; init; }
}

internal class DeleteRepositoryCommandHandler : IRequestHandler<DeleteRepositoryCommand, ApiResponse<DeleteRepositoryResponseDto>>
{
    private IValidator<DeleteRepositoryCommand> _validator;
    private IRepositoryRestClient _repositoryRestClient;
    private IOptions<GiteaApiConfiguration> _giteaOptions;

    public DeleteRepositoryCommandHandler(IValidator<DeleteRepositoryCommand> validator,
        IRepositoryRestClient repositoryRestClient,
        IOptions<GiteaApiConfiguration> giteaOptions)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _repositoryRestClient = repositoryRestClient ?? throw new ArgumentNullException(nameof(repositoryRestClient));
        _giteaOptions = giteaOptions ?? throw new ArgumentNullException(nameof(giteaOptions));
    }

    public async Task<ApiResponse<DeleteRepositoryResponseDto>> Handle(DeleteRepositoryCommand request, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(request);
        return await _repositoryRestClient.DeleteRepositoryAsync(_giteaOptions.Value.RepositoriesOwner, request.RepositoryName);
    }
    
    
}