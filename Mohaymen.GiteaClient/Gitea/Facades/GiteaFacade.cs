using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Mohaymen.GitClient.APICall.Domain;
using Mohaymen.GitClient.Gitea.Business.Commands.Repository.CreateRepository;
using Mohaymen.GitClient.Gitea.Facades.Abstractions;

namespace Mohaymen.GitClient.Gitea.Facades;

internal sealed class GiteaFacade : IGiteaFacade
{
    private readonly IMediator _mediator;

    public GiteaFacade(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public Task<GiteaResponseDto<CreateRepositoryResponseDto>> CreateRepository(CreateRepositoryCommand createRepositoryCommand, CancellationToken cancellationToken)
    {
        return _mediator.Send(createRepositoryCommand, cancellationToken);
    }
}