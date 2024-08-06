using Mohaymen.GiteaClient.Gitea.File.CreateFile.Dtos;
using Refit;

namespace Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData.Abstractions;

public interface ITestFileCreator
{
    Task<ApiResponse<CreateFileResponseDto>> CreateFileAsync(CreateFileCommandDto createFileCommandDto,
        CancellationToken cancellationToken);
}