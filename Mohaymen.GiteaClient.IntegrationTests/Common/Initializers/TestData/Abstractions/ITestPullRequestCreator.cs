using Mohaymen.GiteaClient.Gitea.PullRequest.CreatePullRequest.Dtos;

namespace Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData.Abstractions;

public interface ITestPullRequestCreator
{
    Task CreatePullRequestAsync(CreatePullRequestCommandDto createPullRequestCommandDto, CancellationToken cancellationToken);
}