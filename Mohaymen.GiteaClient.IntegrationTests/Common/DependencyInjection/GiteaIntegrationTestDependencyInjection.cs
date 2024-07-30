using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.IntegrationTests.Common.Assertions;
using Mohaymen.GiteaClient.IntegrationTests.Common.Assertions.Abstractions;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData.Abstractions;
using Mohaymen.GiteaClient.IntegrationTests.Common.Models;

namespace Mohaymen.GiteaClient.IntegrationTests.Common.DependencyInjection;

internal static class GiteaIntegrationTestDependencyInjection
{
    public static IServiceCollection AddGiteaIntegrationTestsServices(this IServiceCollection serviceCollection,
        string baseApiUrl)
    {
        serviceCollection.AddHttpClient(GiteaTestConstants.ApiClientName, httpClient =>
        {
            httpClient.BaseAddress = new Uri(baseApiUrl);
        });
        serviceCollection.AddSingleton<ITestRepositoryCreator, TestRepositoryCreator>();
        serviceCollection.AddSingleton<ITestBranchCreator, TestBranchCreator>();
        serviceCollection.AddSingleton<ITestPullRequestCreator, TestPullRequestCreator>();
        serviceCollection.AddSingleton<ITestRepositoryChecker, TestRepositoryChecker>();
        serviceCollection.AddSingleton<ITestBranchChecker, TestBranchChecker>();
        serviceCollection.AddSingleton<ITestBranchCreator, TestBranchCreator>();
        serviceCollection.AddSingleton<ITestCommitChecker, TestCommitChecker>();
        serviceCollection.AddSingleton<ITestFileChecker, TestFileChecker>();
        serviceCollection.AddSingleton<ITestCommiter, TestCommiter>();
        return serviceCollection;
    }
}