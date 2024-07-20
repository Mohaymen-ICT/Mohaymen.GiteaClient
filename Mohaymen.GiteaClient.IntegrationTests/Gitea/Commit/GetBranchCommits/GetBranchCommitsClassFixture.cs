using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.IntegrationTests.Common.Collections.Gitea;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData.Abstractions;

namespace Mohaymen.GiteaClient.IntegrationTests.Gitea.Commit.GetBranchCommits;

internal class GetBranchCommitsClassFixture : IAsyncLifetime
{
    public const string RepositoryName = "GetBranchCommitsRepo";
    public const string BranchName = "feature/IntegrationTest";
    public const string FilePath = "README1.md";
    public const string CommitMessage = "fakeCommitMessage";
    
    private readonly GiteaCollectionFixture _giteaCollectionFixture;
    private readonly ITestRepositoryCreator _testRepositoryCreator;
    private readonly ITestBranchCreator _testBranchCreator;
    private readonly ITestCommiter _testCommiter;

    public GetBranchCommitsClassFixture(GiteaCollectionFixture giteaCollectionFixture)
    {
        _giteaCollectionFixture = giteaCollectionFixture ?? throw new ArgumentNullException(nameof(giteaCollectionFixture));
        _testRepositoryCreator = giteaCollectionFixture.ServiceProvider.GetRequiredService<ITestRepositoryCreator>();
        _testBranchCreator = giteaCollectionFixture.ServiceProvider.GetRequiredService<ITestBranchCreator>();
        _testCommiter = giteaCollectionFixture.ServiceProvider.GetRequiredService<ITestCommiter>();
    }

    public async Task InitializeAsync()
    {
        await _testRepositoryCreator.CreateRepositoryAsync(RepositoryName, _giteaCollectionFixture.CancellationToken);
        await _testBranchCreator.CreateBranchAsync(RepositoryName, BranchName, _giteaCollectionFixture.CancellationToken);
        await _testCommiter.CreateFileAsync(RepositoryName, BranchName, FilePath, CommitMessage, _giteaCollectionFixture.CancellationToken);
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
}