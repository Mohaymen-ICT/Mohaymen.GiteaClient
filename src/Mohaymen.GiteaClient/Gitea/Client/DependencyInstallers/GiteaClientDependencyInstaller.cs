using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.Core.DependencyInjection.Abstractions;
using Mohaymen.GiteaClient.Gitea.Client.Abstractions;

namespace Mohaymen.GiteaClient.Gitea.Client.DependencyInstallers;

internal class GiteaClientDependencyInstaller : IDependencyInstaller
{
    public void Install(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IGiteaClient, GiteaClient>();
    }
}