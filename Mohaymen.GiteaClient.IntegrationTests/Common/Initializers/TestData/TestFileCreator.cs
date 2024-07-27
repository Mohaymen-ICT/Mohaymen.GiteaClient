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

    public async Task CreateFileAsync(string repositoryName, string filePath, string content, CancellationToken cancellationToken)
    {
        var createFileCommandDto = new CreateFileCommandDto
        {
            RepositoryName = repositoryName,
            FilePath = filePath,
            Content = content
        };

       await _fileFacade.CreateFileAsync(createFileCommandDto, cancellationToken);
    }
}