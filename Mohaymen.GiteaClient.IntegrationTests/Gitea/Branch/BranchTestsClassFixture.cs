using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Mohaymen.GiteaClient.Core.Configs;
using Mohaymen.GiteaClient.IntegrationTests.Common.Collections.Gitea;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData.Abstractions;
using Mohaymen.GiteaClient.IntegrationTests.Common.Models.Requests;
using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.IntegrationTests.Gitea.Branch;

internal sealed class BranchTestsClassFixture : IAsyncLifetime
{
    public const string RepositoryName = "BranchUseCaseRepository";
    
    private readonly ITestRepositoryCreator _testRepositoryCreator;
    
    public BranchTestsClassFixture(GiteaCollectionFixture giteaCollectionFixture)
    {
         _testRepositoryCreator = giteaCollectionFixture.ServiceProvider.GetRequiredService<ITestRepositoryCreator>();
    }

    public async Task InitializeAsync()
    {
        await _testRepositoryCreator.CreateRepository(RepositoryName);
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
}