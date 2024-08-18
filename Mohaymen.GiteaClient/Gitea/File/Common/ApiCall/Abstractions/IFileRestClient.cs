using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Mohaymen.GiteaClient.Core.ApiCall.Abstractions;
using Mohaymen.GiteaClient.Gitea.File.CreateFile.Context;
using Mohaymen.GiteaClient.Gitea.File.CreateFile.Dtos;
using Mohaymen.GiteaClient.Gitea.File.GetRepositoryFile.Context;
using Mohaymen.GiteaClient.Gitea.File.GetRepositoryFile.Dtos;
using Refit;

namespace Mohaymen.GiteaClient.Gitea.File.Common.ApiCall.Abstractions;

internal interface IFileRestClient : IRefitClientInterface
{
    [Get("/repos/{owner}/{repo}/contents?ref={ref}")]
    Task<ApiResponse<List<GetFileResponseDto>>> GetFilesMetadataAsync(string owner,
        [AliasAs("repo")] string repositoryName,
        [AliasAs("ref")] string branchName,
        CancellationToken cancellationToken);

    [Get("/repos/{owner}/{repo}/contents/{filepath}?ref={ref}")]
    Task<ApiResponse<GetFileResponseDto>> GetFileAsync(
        [AliasAs("owner")] string owner,
        [AliasAs("repo")] string repositoryName,
        [AliasAs("filepath")] string filePath,
        [AliasAs("ref")] string refQuery,
        CancellationToken cancellationToken);
    
    [Post("/repos/{owner}/{repo}/contents/{filepath}")]
    Task<ApiResponse<CreateFileResponseDto>> CreateFileAsync(
        [AliasAs("owner")] string owner,
        [AliasAs("repo")] string repositoryName,
        [AliasAs("filepath")] string filePath,
        [Body] CreateFileRequest getFileRequest,
        CancellationToken cancellationToken);
}