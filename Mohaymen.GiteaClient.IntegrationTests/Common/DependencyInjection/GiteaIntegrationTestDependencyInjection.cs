using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData.Abstractions;

namespace Mohaymen.GiteaClient.IntegrationTests.Common.DependencyInjection;

internal static class GiteaIntegrationTestDependencyInjection
{
    public static IServiceCollection AddGiteaIntegrationTestsServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddHttpClient();
        serviceCollection.AddSingleton<ITestRepositoryCreator, TestRepositoryCreator>();
        return serviceCollection;
    }
}