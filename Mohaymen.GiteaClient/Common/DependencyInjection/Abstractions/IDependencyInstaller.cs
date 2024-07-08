using Microsoft.Extensions.DependencyInjection;

namespace Mohaymen.GiteaClient.Common.DependencyInjection.Abstractions;

internal interface IDependencyInstaller
{
    void Install(IServiceCollection serviceCollection);
}