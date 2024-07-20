using Mohaymen.GiteaClient.IntegrationTests.Common.Models.Responses;

namespace Mohaymen.GiteaClient.IntegrationTests.Common.Assertions.Abstractions;

internal interface ITestRepositoryChecker
{
    Task<bool> ContainsRepositoryAsync(string repositoryName);
}