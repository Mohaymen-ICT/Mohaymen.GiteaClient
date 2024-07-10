using System;
using Mohaymen.GiteaClient.Gitea.Client.Abstractions;
using Mohaymen.GiteaClient.Gitea.Repository.Common.Facade.Abstractions;

namespace Mohaymen.GiteaClient.Gitea.Client;

internal class GiteaClient : IGiteaClient
{
    public IRepositoryFacade RepositoryClient { get; }

    public GiteaClient(IRepositoryFacade repositoryFacade)
    {
        RepositoryClient = repositoryFacade ?? throw new ArgumentNullException(nameof(repositoryFacade));
    }
}