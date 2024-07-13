using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.Core.Abstractions;
using Mohaymen.GiteaClient.Core.Configs;
using Mohaymen.GiteaClient.Core.DependencyInjection.Abstractions;

namespace Mohaymen.GiteaClient.Core.DependencyInjection;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddGiteaClient(this IServiceCollection serviceCollection,
        Action<GiteaApiConfiguration> giteaOptionsAction)
    {
        InstallConfigs(serviceCollection, giteaOptionsAction);
        InstallGeneralDefinedDependencies(serviceCollection);
        return serviceCollection;
    }

    private static void InstallConfigs(IServiceCollection serviceCollection,
        Action<GiteaApiConfiguration> giteaOptionsAction)
    {
        serviceCollection.Configure(giteaOptionsAction);
    }
    
    private static void InstallGeneralDefinedDependencies(IServiceCollection serviceCollection)
    {
        var dependencyInstallers = GetGeneralDependencyInstallers();

        foreach (var dependencyInstaller in dependencyInstallers)
        {
            dependencyInstaller.Install(serviceCollection);
        }
    }

    private static IEnumerable<IDependencyInstaller> GetGeneralDependencyInstallers()
    {
        return typeof(IAssemblyMarkerInterface)
            .Assembly
            .DefinedTypes
            .Where(type => !type.IsAbstract && !type.IsInterface && typeof(IDependencyInstaller).IsAssignableFrom(type))
            .Select(Activator.CreateInstance)
            .Cast<IDependencyInstaller>();
    }
}