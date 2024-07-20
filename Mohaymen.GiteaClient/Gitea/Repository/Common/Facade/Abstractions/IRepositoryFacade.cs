using System.Threading;
using System.Threading.Tasks;
using Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Dtos;
using Mohaymen.GiteaClient.Gitea.Repository.DeleteRepository.Dto;
using Mohaymen.GiteaClient.Gitea.Repository.SearchRepository.Dtos;
using Refit;

namespace Mohaymen.GiteaClient.Gitea.Repository.Common.Facade.Abstractions;

public interface IRepositoryFacade
{
    Task<ApiResponse<CreateRepositoryResponseDto>> CreateRepositoryAsync(CreateRepositoryCommandDto createRepositoryCommandDto, CancellationToken cancellationToken);
    Task<ApiResponse<SearchRepositoryResponseDto>> SearchRepositoryAsync(SearchRepositoryQueryDto searchRepositoryQueryDto, CancellationToken cancellationToken);

    Task<ApiResponse<DeleteRepositoryResponseDto>> DeleteRepositoryAsync(DeleteRepositoryCommandDto deleteRepositoryCommandDto,
        CancellationToken cancellationToken);
}