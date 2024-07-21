namespace Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData.Abstractions;

internal interface ITestRepositoryCreator
{
    Task CreateRepository(string repositoryName);
}