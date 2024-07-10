using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.Core.Configs;

namespace Mohaymen.GiteaClient.Core.DependencyInjection.Abstractions;

internal interface IRefitClientDependencyInstaller
{
    void Install(IServiceCollection serviceCollection, GiteaApiConfiguration giteaApiConfiguration);
}