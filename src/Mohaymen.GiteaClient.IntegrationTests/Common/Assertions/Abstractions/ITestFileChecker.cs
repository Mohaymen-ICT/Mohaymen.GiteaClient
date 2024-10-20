namespace Mohaymen.GiteaClient.IntegrationTests.Common.Assertions.Abstractions;

internal interface ITestFileChecker
{
    Task<bool> ContainsFileAsync(string repositoryName, string fileName, CancellationToken cancellationToken);
    Task<bool> HasFileContent(string repositoryName, string filePath, string content, CancellationToken cancellationToken);
}