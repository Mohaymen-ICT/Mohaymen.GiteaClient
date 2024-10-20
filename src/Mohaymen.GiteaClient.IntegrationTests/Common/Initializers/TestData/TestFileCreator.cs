using Mohaymen.GiteaClient.Gitea.File.Common.Facade.Abstractions;
using Mohaymen.GiteaClient.Gitea.File.CreateFile.Dtos;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData.Abstractions;
using Refit;

namespace Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData;

public class TestFileCreator : ITestFileCreator
{
    private readonly IFileFacade _fileFacade;

    public TestFileCreator(IFileFacade fileFacade)
    {
        _fileFacade = fileFacade ?? throw new ArgumentNullException(nameof(fileFacade));
    }

    public async Task<ApiResponse<CreateFileResponseDto>> CreateFileAsync(CreateFileCommandDto createFileCommandDto,
        CancellationToken cancellationToken)
    {
       return await _fileFacade.CreateFileAsync(createFileCommandDto, cancellationToken);
    }
}