using Mohaymen.GiteaClient.Gitea.Repository.Common.Facade.Abstractions;

namespace Mohaymen.GiteaClient.Gitea.Client.Abstractions;

public interface IGiteaClient
{
    IRepositoryFacade RepositoryClient { get; }
}