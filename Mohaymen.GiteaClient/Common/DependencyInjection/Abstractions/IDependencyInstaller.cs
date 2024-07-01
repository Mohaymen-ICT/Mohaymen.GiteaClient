using Microsoft.Extensions.DependencyInjection;

namespace Mohaymen.GitClient.Common.DependencyInjection.Abstractions;

internal interface IDependencyInstaller
{
    void Install(IServiceCollection serviceCollection);
}