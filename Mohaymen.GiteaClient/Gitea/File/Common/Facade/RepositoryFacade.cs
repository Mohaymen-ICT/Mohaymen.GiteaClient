using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Mohaymen.GiteaClient.Gitea.File.Common.Facade.Abstractions;
using Mohaymen.GiteaClient.Gitea.File.GetRepositoryFile.Dtos;
using Mohaymen.GiteaClient.Gitea.File.GetRepositoryFile.Mappers;
using Refit;

namespace Mohaymen.GiteaClient.Gitea.File.Common.Facade;

internal class FileFacade : IFileFacade
{
    private readonly IMediator _mediator;

    public FileFacade(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task<ApiResponse<GetFileResponseDto>> GetFileAsync(GetFileCommandDto getFileCommandDto,
        CancellationToken cancellationToken)
    {
        var command = getFileCommandDto.ToGetRepositoryFileCommand();
        return await _mediator.Send(command, cancellationToken);
    }
}