using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.IntegrationTests.Common.Collections.Gitea;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData.Abstractions;
using Mohaymen.GiteaClient.IntegrationTests.Common.Models;

namespace Mohaymen.GiteaClient.IntegrationTests.Gitea.Branch.CreateBranch;

public class CreateBranchTestsClassFixture : IAsyncLifetime
{
    private readonly ITestRepositoryCreator _testRepositoryCreator;
    
    public CreateBranchTestsClassFixture(GiteaCollectionFixture giteaCollectionFixture)
    {
         _testRepositoryCreator = giteaCollectionFixture.ServiceProvider.GetRequiredService<ITestRepositoryCreator>();
    }

    public async Task InitializeAsync()
    {
        await _testRepositoryCreator.CreateRepository(GiteaTestConstants.RepositoryName);
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
}