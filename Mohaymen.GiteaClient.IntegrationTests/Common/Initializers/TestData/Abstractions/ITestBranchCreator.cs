namespace Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData.Abstractions;

public interface ITestBranchCreator
{
    Task CreateBranch(string repositoryName, string branchName);
}