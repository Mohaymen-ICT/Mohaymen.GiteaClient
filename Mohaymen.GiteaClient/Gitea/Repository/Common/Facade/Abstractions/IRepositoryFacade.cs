using System.Threading;
using System.Threading.Tasks;
using Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Dtos;
using Refit;

namespace Mohaymen.GiteaClient.Gitea.Repository.Common.Facade.Abstractions;

public interface IRepositoryFacade
{
    Task<ApiResponse<CreateRepositoryResponseDto>> CreateRepositoryAsync(CreateRepositoryCommandDto createRepositoryCommandDto, CancellationToken cancellationToken);
}