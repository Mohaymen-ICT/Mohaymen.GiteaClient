namespace Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData.Abstractions;

internal interface ITestCommiter
{
    Task CreateFileAsync(string repositoryName,
        string branchName,
        string filePath,
        string commitMessage,
        CancellationToken cancellationToken);
}