﻿using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Mohaymen.GiteaClient.Gitea.File.Common.Facade.Abstractions;
using Mohaymen.GiteaClient.Gitea.File.CreateFile.Dtos;
using Mohaymen.GiteaClient.Gitea.File.CreateFile.Mappers;
using Mohaymen.GiteaClient.Gitea.File.GetFile.Mappers;
using Mohaymen.GiteaClient.Gitea.File.GetRepositoryFile.Dtos;
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
        var command = getFileCommandDto.ToGetFileCommand();
        return await _mediator.Send(command, cancellationToken);
    }

    public async Task<ApiResponse<CreateFileResponseDto>> CreateFileAsync(CreateFileCommandDto createFileCommandDto, 
        CancellationToken cancellationToken)
    {
        var command = createFileCommandDto.Map();
        return await _mediator.Send(command, cancellationToken);
    }
}