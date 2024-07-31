using System.Threading;
using System.Threading.Tasks;
using Mohaymen.GiteaClient.Gitea.File.CreateFile.Dtos;
using Mohaymen.GiteaClient.Gitea.File.GetRepositoryFile.Dtos;
using Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Dtos;
using Refit;

namespace Mohaymen.GiteaClient.Gitea.File.Common.Facade.Abstractions;

public interface IFileFacade
{
    Task<ApiResponse<GetFileResponseDto>> GetFileAsync(GetFileCommandDto getFileCommandDto, CancellationToken cancellationToken);
    Task<ApiResponse<CreateFileResponseDto>> CreateFileAsync(CreateFileCommandDto createFileCommandDto, CancellationToken cancellationToken);
}