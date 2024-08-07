using Mohaymen.GiteaClient.Gitea.Branch.Common.Facade.Abstractions;
using Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Dtos;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData.Abstractions;
using Mohaymen.GiteaClient.IntegrationTests.Common.Models;

namespace Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData;

public class TestBranchCreator : ITestBranchCreator
{
    private readonly IBranchFacade _branchFacade;
    
    public TestBranchCreator(IBranchFacade branchFacade)
    {
        _branchFacade = branchFacade ?? throw new ArgumentNullException(nameof(branchFacade));
    }
    
    public async Task CreateBranchAsync(string repositoryName, string branchName,
        CancellationToken cancellationToken)
    {
        try
        {
            var createBranchCommandDto = new CreateBranchCommandDto
            {
                RepositoryName = repositoryName,
                NewBranchName = branchName,
                OldReferenceName = GiteaTestConstants.DefaultBranch
            };
            await _branchFacade.CreateBranchAsync(createBranchCommandDto, cancellationToken);
        }
        catch (Exception)
        {
        }
    }
}