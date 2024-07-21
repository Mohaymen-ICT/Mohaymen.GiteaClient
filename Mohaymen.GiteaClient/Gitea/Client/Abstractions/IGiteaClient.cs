using Mohaymen.GiteaClient.Gitea.Branch.Common.Facade.Abstractions;
using Mohaymen.GiteaClient.Gitea.Repository.Common.Facade.Abstractions;

namespace Mohaymen.GiteaClient.Gitea.Client.Abstractions;

public interface IGiteaClient
{
    IRepositoryFacade RepositoryClient { get; }
    IBranchFacade BranchClient { get; }
}