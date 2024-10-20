namespace Mohaymen.GiteaClient.IntegrationTests.Common.Assertions.Abstractions;

internal interface ITestCommitChecker
{
    Task<bool> ContainsCommitWithShaAsync(string repositoryName,
        string branchName,
        string commitSha,
        CancellationToken cancellationToken);
}