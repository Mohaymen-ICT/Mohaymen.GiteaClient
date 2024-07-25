using Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Dtos;
using Refit;

namespace Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData.Abstractions;

internal interface ITestRepositoryCreator
{
    Task CreateRepositoryAsync(string repositoryName, CancellationToken cancellationToken);
}