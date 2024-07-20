using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.IntegrationTests.Common.Collections.Gitea;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData.Abstractions;

namespace Mohaymen.GiteaClient.IntegrationTests.Gitea.Branch;

public class BranchTestsClassFixture : IAsyncLifetime
{
    public const string RepositoryName = "BranchUseCaseRepository";
    
    private readonly ITestRepositoryCreator _testRepositoryCreator;
    private readonly GiteaCollectionFixture _giteaCollectionFixture;
    
    public BranchTestsClassFixture(GiteaCollectionFixture giteaCollectionFixture)
    {
        _giteaCollectionFixture = giteaCollectionFixture ?? throw new ArgumentNullException(nameof(giteaCollectionFixture));
        _testRepositoryCreator = giteaCollectionFixture.ServiceProvider.GetRequiredService<ITestRepositoryCreator>();
    }

    public async Task InitializeAsync()
    {
        await _testRepositoryCreator.CreateRepositoryAsync(RepositoryName, _giteaCollectionFixture.CancellationToken);
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
}