using Mohaymen.GiteaClient.Gitea.File.GetRepositoryFile.Dtos;

namespace Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData.Abstractions;

internal interface ITestFileMetadataGetter
{
    Task<GetFileResponseDto> GetFileMetadataAsync(string repositoryName,
        string filePath,
        CancellationToken cancellationToken);
}