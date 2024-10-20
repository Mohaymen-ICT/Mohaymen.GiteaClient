using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Mohaymen.GiteaClient.Gitea.File.CreateFile.Dtos;
using Mohaymen.GiteaClient.Gitea.File.GetFilesMetadata.Dto;
using Mohaymen.GiteaClient.Gitea.File.GetRepositoryFile.Dtos;
using Refit;

namespace Mohaymen.GiteaClient.Gitea.File.Common.Facade.Abstractions;

public interface IFileFacade
{
    Task<ApiResponse<List<GetFileResponseDto>>> GetFilesMetadataAsync(GetFilesMetadataQueryDto getFilesMetadataQuery, CancellationToken cancellationToken);
    Task<ApiResponse<GetFileResponseDto>> GetFileAsync(GetFileMetadataQueryDto getFileMetadataQueryDto, CancellationToken cancellationToken);
    Task<ApiResponse<CreateFileResponseDto>> CreateFileAsync(CreateFileCommandDto createFileCommandDto, CancellationToken cancellationToken);
}