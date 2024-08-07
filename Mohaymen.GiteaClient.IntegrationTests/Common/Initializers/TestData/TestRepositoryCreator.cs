using Mohaymen.GiteaClient.Gitea.Repository.Common.Facade.Abstractions;
using Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Dtos;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData.Abstractions;
using Mohaymen.GiteaClient.IntegrationTests.Common.Models;
using Refit;

namespace Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData;

internal class TestRepositoryCreator : ITestRepositoryCreator
{
    private readonly IRepositoryFacade _repositoryFacade;

    public TestRepositoryCreator(IRepositoryFacade repositoryFacade)
    {
        _repositoryFacade = repositoryFacade ?? throw new ArgumentNullException(nameof(repositoryFacade));
    }
    
    public async Task CreateRepositoryAsync(string repositoryName, 
        CancellationToken cancellationToken)
    {
        try
        {
            var createRepositoryCommandDto = new CreateRepositoryCommandDto
            {
                DefaultBranch = GiteaTestConstants.DefaultBranch,
                Name = repositoryName,
                IsPrivateBranch = true,
                AutoInit = true
            };
            await _repositoryFacade.CreateRepositoryAsync(createRepositoryCommandDto, cancellationToken);
        }
        catch (Exception)
        {
        }
    }
}