using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GitClient.Common.DependencyInjection.Abstractions;
using Mohaymen.GitClient.Gitea.Facades;
using Mohaymen.GitClient.Gitea.Facades.Abstractions;

namespace Mohaymen.GitClient.Gitea.DependencyInstallers;

internal class GiteaDependencyInstaller : IDependencyInstaller
{
    public void Install(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IGiteaFacade, GiteaFacade>();
    }
}