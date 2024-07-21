using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.IntegrationTests.Common.Collections.Gitea;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData.Abstractions;

namespace Mohaymen.GiteaClient.IntegrationTests.Gitea.Commit.CreateCommit;

internal class CreateCommitClassFixture : IAsyncLifetime
{
    public const string RepositoryName = "CreateCommitRepository";
    public const string FileDelete = "SampleDeleteFile.txt";
    
    private readonly ITestRepositoryCreator _testRepositoryCreator;
    private readonly ITestCommiter _testCommiter;
    private readonly GiteaCollectionFixture _giteaCollectionFixture;

    public CreateCommitClassFixture(GiteaCollectionFixture giteaCollectionFixture)
    {
        _giteaCollectionFixture = giteaCollectionFixture ?? throw new ArgumentNullException(nameof(giteaCollectionFixture));
        _testRepositoryCreator = giteaCollectionFixture.ServiceProvider.GetRequiredService<ITestRepositoryCreator>();
        _testCommiter = giteaCollectionFixture.ServiceProvider.GetRequiredService<ITestCommiter>();
    }

    public async Task InitializeAsync()
    {
        await _testRepositoryCreator.CreateRepositoryAsync(RepositoryName, _giteaCollectionFixture.CancellationToken);
        await _testCommiter.CreateFileAsync(RepositoryName,
            "main",
            FileDelete,
            "ccc",
            _giteaCollectionFixture.CancellationToken);
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
}