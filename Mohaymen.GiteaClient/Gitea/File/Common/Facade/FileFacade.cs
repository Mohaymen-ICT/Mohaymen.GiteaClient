using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Mohaymen.GiteaClient.Commons.Observability.Abstraction;
using Mohaymen.GiteaClient.Gitea.File.Common.Facade.Abstractions;
using Mohaymen.GiteaClient.Gitea.File.CreateFile.Dtos;
using Mohaymen.GiteaClient.Gitea.File.CreateFile.Mappers;
using Mohaymen.GiteaClient.Gitea.File.GetFile.Mappers;
using Mohaymen.GiteaClient.Gitea.File.GetFilesMetadata.Dto;
using Mohaymen.GiteaClient.Gitea.File.GetFilesMetadata.Mappers;
using Mohaymen.GiteaClient.Gitea.File.GetRepositoryFile.Dtos;
using Refit;

namespace Mohaymen.GiteaClient.Gitea.File.Common.Facade;

internal class FileFacade : IFileFacade
{
	private readonly IMediator _mediator;
	private readonly ITraceInstrumentation _traceInstrumentation;

	public FileFacade(IMediator mediator, ITraceInstrumentation traceInstrumentation)
	{
		_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
		_traceInstrumentation = traceInstrumentation ?? throw new ArgumentNullException(nameof(traceInstrumentation));
	}

	public async Task<ApiResponse<List<GetFileResponseDto>>> GetFilesMetadataAsync(GetFilesMetadataQueryDto getFilesMetadataQuery, CancellationToken cancellationToken)
	{
		var query = GetFilesMetadataQueryMapper.Map(getFilesMetadataQuery);
		return await _mediator.Send(query, cancellationToken);
	}

	public async Task<ApiResponse<GetFileResponseDto>> GetFileAsync(GetFileMetadataQueryDto getFileMetadataQueryDto,
		CancellationToken cancellationToken)
	{
		using var activity = _traceInstrumentation.ActivitySource.StartActivity("GetFile", ActivityKind.Internal);
		var command = getFileMetadataQueryDto.ToGetFileCommand();
		return await _mediator.Send(command, cancellationToken);
	}

	public async Task<ApiResponse<CreateFileResponseDto>> CreateFileAsync(CreateFileCommandDto createFileCommandDto,
		CancellationToken cancellationToken)
	{
		using var activity = _traceInstrumentation.ActivitySource.StartActivity("CreateFile", ActivityKind.Internal);
		var command = createFileCommandDto.Map();
		return await _mediator.Send(command, cancellationToken);
	}
}
