using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.IntegrationTests.Common.Collections.Gitea;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData.Abstractions;

namespace Mohaymen.GiteaClient.IntegrationTests.Gitea.Repository;

internal sealed class RepositoryClassFixture : IAsyncLifetime
{
    public const string SearchRepositoryName = "SearchUseCaseRepository";
    public const string DeleteRepositoryName = "DeleteUseCaseRepository";
    
    private readonly ITestRepositoryCreator _testRepositoryCreator;

    public RepositoryClassFixture(GiteaCollectionFixture giteaCollectionFixture)
    {
        _testRepositoryCreator = giteaCollectionFixture.ServiceProvider.GetRequiredService<ITestRepositoryCreator>();
    }
    
    public async Task InitializeAsync()
    {
        await _testRepositoryCreator.CreateRepository(SearchRepositoryName);
        await _testRepositoryCreator.CreateRepository(DeleteRepositoryName);
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
}