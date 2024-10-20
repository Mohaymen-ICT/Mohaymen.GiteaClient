using Mohaymen.GiteaClient.Gitea.File.Common.Facade.Abstractions;
using Mohaymen.GiteaClient.Gitea.File.GetRepositoryFile.Dtos;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData.Abstractions;

namespace Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData;

internal class TestFileMetadataGetter : ITestFileMetadataGetter
{
    private readonly IFileFacade _fileFacade;

    public TestFileMetadataGetter(IFileFacade fileFacade)
    {
        _fileFacade = fileFacade ?? throw new ArgumentNullException(nameof(fileFacade));
    }

    public async Task<GetFileResponseDto> GetFileMetadataAsync(string repositoryName,
        string filePath,
        CancellationToken cancellationToken)
    {
        var fileMetadata = await _fileFacade.GetFileAsync(new GetFileMetadataQueryDto
        {
            RepositoryName = repositoryName,
            FilePath = filePath
        }, cancellationToken).ConfigureAwait(false);
        return fileMetadata.Content!;
    }
}