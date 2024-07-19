using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.IntegrationTests.Common.Collections.Gitea;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData.Abstractions;

namespace Mohaymen.GiteaClient.IntegrationTests.Gitea.Repository;

internal sealed class RepositoryClassFixture : IAsyncLifetime
{
    public const string RepositoryName = "RepositoryUseCaseRepository"; 
    
    private readonly ITestRepositoryCreator _testRepositoryCreator;

    public RepositoryClassFixture(GiteaCollectionFixture giteaCollectionFixture)
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