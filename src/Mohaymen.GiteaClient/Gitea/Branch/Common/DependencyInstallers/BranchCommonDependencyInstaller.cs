using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.Core.DependencyInjection.Abstractions;
using Mohaymen.GiteaClient.Gitea.Branch.Common.Facade;
using Mohaymen.GiteaClient.Gitea.Branch.Common.Facade.Abstractions;

namespace Mohaymen.GiteaClient.Gitea.Branch.Common.DependencyInstallers;

internal class BranchCommonDependencyInstaller : IDependencyInstaller
{
    public void Install(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IBranchFacade, BranchFacade>();
    }
}