namespace Mohaymen.GiteaClient.IntegrationTests.Common.Assertions.Abstractions;

internal interface ITestBranchChecker
{
    Task<bool> ContainsBranch(string repositoryName, string branchName, CancellationToken cancellationToken);
}