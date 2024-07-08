using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.Common.DependencyInjection.Abstractions;
using Mohaymen.GiteaClient.Gitea.Facades;
using Mohaymen.GiteaClient.Gitea.Facades.Abstractions;

namespace Mohaymen.GiteaClient.Gitea;

internal class GiteaDependencyInstaller : IDependencyInstaller
{
    public void Install(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IGiteaFacade, GiteaFacade>();
    }
}