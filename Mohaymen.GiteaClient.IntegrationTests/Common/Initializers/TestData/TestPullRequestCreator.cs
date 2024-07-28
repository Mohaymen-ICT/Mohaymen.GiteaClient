using Mohaymen.GiteaClient.Gitea.PullRequest.Common.Facade.Abstractions;
using Mohaymen.GiteaClient.Gitea.PullRequest.CreatePullRequest.Dtos;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData.Abstractions;

namespace Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData;

internal class TestPullRequestCreator : ITestPullRequestCreator
{
    private readonly IPullRequestFacade _pullRequestFacade;

    public TestPullRequestCreator(IPullRequestFacade pullRequestFacade)
    {
        _pullRequestFacade = pullRequestFacade ?? throw new ArgumentNullException(nameof(pullRequestFacade));
    }
    
    public async Task CreatePullRequestAsync(CreatePullRequestCommandDto createPullRequestCommandDto,
        CancellationToken cancellationToken)
    {
        await _pullRequestFacade.CreatePullRequestAsync(createPullRequestCommandDto, cancellationToken);
    }
}