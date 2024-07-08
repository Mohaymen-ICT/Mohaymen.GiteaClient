using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Mohaymen.GiteaClient.APICall.Domain;
using Mohaymen.GiteaClient.Gitea.Business.Mappers.Repository;
using Mohaymen.GiteaClient.Gitea.Domain.Dtos.Repository.CreateRepository;
using Mohaymen.GiteaClient.Gitea.Facades.Abstractions;

namespace Mohaymen.GiteaClient.Gitea.Facades;

internal sealed class GiteaFacade : IGiteaFacade
{
    private readonly IMediator _mediator;
    private readonly IValidator<CreateRepositoryCommandDto> _createRepositoryModelValidator;

    public GiteaFacade(IMediator mediator, 
        IValidator<CreateRepositoryCommandDto> createRepositoryModelValidator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _createRepositoryModelValidator = createRepositoryModelValidator;
    }

    public async Task<GiteaResponseDto<CreateRepositoryResponseDto>> CreateRepository(CreateRepositoryCommandDto createRepositoryCommandDto, CancellationToken cancellationToken)
    {
        _createRepositoryModelValidator.ValidateAndThrow(createRepositoryCommandDto);
        var createRepositoryCommand = CreateRepositoryMapper.Map(createRepositoryCommandDto);
        return await _mediator.Send(createRepositoryCommand, cancellationToken);
    }
}