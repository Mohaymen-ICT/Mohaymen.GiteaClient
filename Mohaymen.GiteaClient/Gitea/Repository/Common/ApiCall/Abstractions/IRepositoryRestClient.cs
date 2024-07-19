using System.Threading.Tasks;
using Mohaymen.GiteaClient.Core.ApiCall.Abstractions;
using Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Context;
using Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Dtos;
using Mohaymen.GiteaClient.Gitea.Repository.SearchRepository.Context;
using Mohaymen.GiteaClient.Gitea.Repository.SearchRepository.Dtos;
using Mohaymen.GiteaClient.Gitea.Repository.SearchRepository.Queries;
using Refit;

namespace Mohaymen.GiteaClient.Gitea.Repository.Common.ApiCall.Abstractions;

internal interface IRepositoryRestClient : IRefitClientInterface
{
    [Post("/user/repos")]
    Task<ApiResponse<CreateRepositoryResponseDto>> CreateRepositoryAsync([Body] CreateRepositoryRequest createRepositoryRequest);

    [Get("repos/search")]
    Task<ApiResponse<SearchRepositoryResponseDto>> SearchRepositoryAsync([Body] SearchRepositoryRequest searchRepositoryRequest);
}