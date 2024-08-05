using Mohaymen.GiteaClient.Gitea.File.CreateFile.Dtos;

namespace Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData.Abstractions;

public interface ITestFileCreator
{
    Task CreateFileAsync(CreateFileCommandDto createFileCommandDto, CancellationToken cancellationToken);
}