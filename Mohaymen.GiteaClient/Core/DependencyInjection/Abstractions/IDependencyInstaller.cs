using Microsoft.Extensions.DependencyInjection;

namespace Mohaymen.GiteaClient.Core.DependencyInjection.Abstractions;

internal interface IDependencyInstaller
{
    void Install(IServiceCollection serviceCollection);
}