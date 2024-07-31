namespace Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData.Abstractions;

public interface ITestFileCreator
{
    Task CreateFileAsync(string repositoryName, string filePath, string content, CancellationToken cancellationToken);
}