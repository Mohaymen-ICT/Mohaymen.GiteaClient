using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Dtos.Response;
using Refit;

namespace Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData.Abstractions;

internal interface ITestCommiter
{
    Task<ApiResponse<CreateCommitResponseDto>?> CreateFileAsync(string repositoryName,
        string branchName,
        string filePath,
        string commitMessage,
        CancellationToken cancellationToken);
}