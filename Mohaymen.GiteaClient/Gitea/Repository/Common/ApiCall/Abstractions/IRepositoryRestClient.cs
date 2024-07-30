using System.Threading.Tasks;
using Mohaymen.GiteaClient.Core.ApiCall.Abstractions;
using Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Context;
using Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Dtos;
using Mohaymen.GiteaClient.Gitea.Repository.DeleteRepository.Dto;
using Mohaymen.GiteaClient.Gitea.Repository.SearchRepository.Dtos;
using Refit;

namespace Mohaymen.GiteaClient.Gitea.Repository.Common.ApiCall.Abstractions;

internal interface IRepositoryRestClient : IRefitClientInterface
{
    [Post("/user/repos")]
    Task<ApiResponse<CreateRepositoryResponseDto>> CreateRepositoryAsync([Body] CreateRepositoryRequest createRepositoryRequest);

    [Get("/repos/search?q={query}&page={page}&limit={limit}")]
    Task<ApiResponse<SearchRepositoryResponseDto>> SearchRepositoryAsync(string query, int page, int limit);

    [Delete("/repos/{owner}/{repositoryName}")]
    Task<ApiResponse<DeleteRepositoryResponseDto>> DeleteRepositoryAsync(string owner, string repositoryName);
}