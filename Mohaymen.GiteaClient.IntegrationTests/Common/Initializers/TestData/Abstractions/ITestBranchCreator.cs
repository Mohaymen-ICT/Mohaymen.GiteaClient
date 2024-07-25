using Mohaymen.GiteaClient.Gitea.Branch.Common.Dtos;
using Refit;

namespace Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData.Abstractions;

public interface ITestBranchCreator
{
    Task CreateBranchAsync(string repositoryName, string branchName, CancellationToken cancellationToken);
}