using Mohaymen.GiteaClient.Gitea.Branch.Common.Facade.Abstractions;
using Mohaymen.GiteaClient.Gitea.Commit.Common.Facades.Abstractions;
using Mohaymen.GiteaClient.Gitea.File.Common.Facade.Abstractions;
using Mohaymen.GiteaClient.Gitea.PullRequest.Common.Facade.Abstractions;
using Mohaymen.GiteaClient.Gitea.Repository.Common.Facade.Abstractions;

namespace Mohaymen.GiteaClient.Gitea.Client.Abstractions;

public interface IGiteaClient
{
    ICommitFacade CommitClient { get; }
    IRepositoryFacade RepositoryClient { get; }
    IBranchFacade BranchClient { get; }
    IPullRequestFacade PullRequestClient { get; }
    IFileFacade FileClient { get; }
}