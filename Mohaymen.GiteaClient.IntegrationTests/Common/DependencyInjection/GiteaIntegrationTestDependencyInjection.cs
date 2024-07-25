﻿using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.Gitea.Branch.Common.Facade;
using Mohaymen.GiteaClient.Gitea.Branch.Common.Facade.Abstractions;
using Mohaymen.GiteaClient.Gitea.Repository.Common.Facade;
using Mohaymen.GiteaClient.Gitea.Repository.Common.Facade.Abstractions;
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
        serviceCollection.AddSingleton<ITestRepositoryChecker, TestRepositoryChecker>();
        serviceCollection.AddSingleton<ITestBranchChecker, TestBranchChecker>();
        return serviceCollection;
    }
}