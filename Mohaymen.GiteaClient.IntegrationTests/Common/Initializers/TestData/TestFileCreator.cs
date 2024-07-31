using Mohaymen.GiteaClient.Gitea.File.Common.Facade.Abstractions;
using Mohaymen.GiteaClient.Gitea.File.CreateFile.Dtos;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData.Abstractions;

namespace Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData;

public class TestFileCreator : ITestFileCreator
{
    private readonly IFileFacade _fileFacade;

    public TestFileCreator(IFileFacade fileFacade)
    {
        _fileFacade = fileFacade ?? throw new ArgumentNullException(nameof(fileFacade));
    }

    public async Task CreateFileAsync(CreateFileCommandDto createFileCommandDto, CancellationToken cancellationToken)
    {
       await _fileFacade.CreateFileAsync(createFileCommandDto, cancellationToken);
    }
}