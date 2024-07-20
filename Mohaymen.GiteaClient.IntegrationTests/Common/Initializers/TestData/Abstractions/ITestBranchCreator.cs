namespace Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData.Abstractions;

internal interface ITestBranchCreator
{
    Task CreateBranchAsync(string repositoryName, string branchName, CancellationToken cancellationToken);
}