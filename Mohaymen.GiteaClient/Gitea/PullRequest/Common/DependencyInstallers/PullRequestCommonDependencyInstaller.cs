using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.Core.DependencyInjection.Abstractions;
using Mohaymen.GiteaClient.Gitea.PullRequest.Common.Facade;
using Mohaymen.GiteaClient.Gitea.PullRequest.Common.Facade.Abstractions;

namespace Mohaymen.GiteaClient.Gitea.PullRequest.Common.DependencyInstallers;

internal class PullRequestCommonDependencyInstaller : IDependencyInstaller
{
    public void Install(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IPullRequestFacade, PullRequestFacade>();
    }
}