using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.Core.DependencyInjection.Abstractions;
using Mohaymen.GiteaClient.Gitea.File.Common.Encoding;
using Mohaymen.GiteaClient.Gitea.File.Common.Encoding.Abstraction;
using Mohaymen.GiteaClient.Gitea.File.Common.Facade;
using Mohaymen.GiteaClient.Gitea.File.Common.Facade.Abstractions;

namespace Mohaymen.GiteaClient.Gitea.File.Common.DependencyInstallers;

internal class FileCommonDependencyInstaller : IDependencyInstaller
{
    public void Install(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IFileFacade, FileFacade>();
        serviceCollection.AddSingleton<IContentEncoder, ContentEncoder>();
        serviceCollection.AddSingleton<IContentDecoder, ContentDecoder>();
    }
}