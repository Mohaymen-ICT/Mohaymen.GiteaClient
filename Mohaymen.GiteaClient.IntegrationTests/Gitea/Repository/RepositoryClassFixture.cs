using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.IntegrationTests.Common.Collections.Gitea;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData.Abstractions;

namespace Mohaymen.GiteaClient.IntegrationTests.Gitea.Repository;

internal sealed class RepositoryClassFixture : IAsyncLifetime
{
    public const string SearchRepositoryName = "SearchUseCaseRepository";
    public const string DeleteRepositoryName = "DeleteUseCaseRepository";
    
    private readonly ITestRepositoryCreator _testRepositoryCreator;
    private readonly GiteaCollectionFixture _giteaCollectionFixture;

    public RepositoryClassFixture(GiteaCollectionFixture giteaCollectionFixture)
    {
        _giteaCollectionFixture = giteaCollectionFixture ?? throw new ArgumentNullException(nameof(giteaCollectionFixture));
        _testRepositoryCreator = giteaCollectionFixture.ServiceProvider.GetRequiredService<ITestRepositoryCreator>();
    }
    
    public async Task InitializeAsync()
    {
        await _testRepositoryCreator.CreateRepositoryAsync(SearchRepositoryName, _giteaCollectionFixture.CancellationToken);
        await _testRepositoryCreator.CreateRepositoryAsync(DeleteRepositoryName, _giteaCollectionFixture.CancellationToken);
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
}