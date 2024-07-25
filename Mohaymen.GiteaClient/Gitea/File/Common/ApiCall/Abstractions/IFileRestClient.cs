using System.Threading.Tasks;
using Mohaymen.GiteaClient.Core.ApiCall.Abstractions;
using Mohaymen.GiteaClient.Gitea.File.GetRepositoryFile.Context;
using Mohaymen.GiteaClient.Gitea.File.GetRepositoryFile.Dtos;
using Refit;

namespace Mohaymen.GiteaClient.Gitea.File.Common.ApiCall.Abstractions;

internal interface IFileRestClient : IRefitClientInterface
{
    [Get("/repos/{owner}/{repo}/contents/{filepath}")]
    Task<ApiResponse<GetFileResponseDto>> GetFileAsync(
        [AliasAs("owner")] string owner,
        [AliasAs("repo")] string repositoryName,
        [AliasAs("filepath")] string filePath,
        [Body] GetFileRequest getFileRequest);
}