using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.IntegrationTests.Common.Collections.Gitea;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData.Abstractions;
using Mohaymen.GiteaClient.IntegrationTests.Common.Models;

namespace Mohaymen.GiteaClient.IntegrationTests.Gitea.Branch.GetBranchList;

public class GetBranchListTestsClassFixture : IAsyncLifetime
{
    public const string FirstBranch = "branch1";
    public const string SecondBranch = "branch2";
    public const string ThirdBranch = "branch3";
    
    private readonly ITestRepositoryCreator _testRepositoryCreator;
    private readonly ITestBranchCreator _testBranchCreator;
    
    public GetBranchListTestsClassFixture(GiteaCollectionFixture giteaCollectionFixture)
    {
        _testRepositoryCreator = giteaCollectionFixture.ServiceProvider.GetRequiredService<ITestRepositoryCreator>();
        _testBranchCreator = giteaCollectionFixture.ServiceProvider.GetRequiredService<ITestBranchCreator>();
    }

    public async Task InitializeAsync()
    {
        var repo = GiteaTestConstants.RepositoryName;
        await _testRepositoryCreator.CreateRepository(repo);
        await _testBranchCreator.CreateBranch(repo, FirstBranch);
        await _testBranchCreator.CreateBranch(repo, SecondBranch);
        await _testBranchCreator.CreateBranch(repo, ThirdBranch);
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
}