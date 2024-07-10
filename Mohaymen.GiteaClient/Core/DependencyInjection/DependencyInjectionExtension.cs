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
    public static IServiceCollection AddGiteaClientServices(this IServiceCollection serviceCollection, Action<GiteaApiConfiguration> giteaOptionsAction)
    {
        InstallGeneralDefinedDependencies(serviceCollection);
        InstallRefitRestClients(serviceCollection, giteaOptionsAction);
        return serviceCollection;
    }

    private static void InstallRefitRestClients(IServiceCollection serviceCollection, Action<GiteaApiConfiguration> giteaOptionsAction)
    {
        var refitClientInstallers = GetRefitClientDependencyInstallers();
        var giteaClientConfiguration = new GiteaApiConfiguration
        {
            BaseUrl = "",
            PersonalAccessToken = "",
            RepositoriesOwner = ""
        };
        giteaOptionsAction(giteaClientConfiguration);
        foreach (var refitClientInstaller in refitClientInstallers)
        {
            refitClientInstaller.Install(serviceCollection, giteaClientConfiguration);
        }
    }

    private static IEnumerable<IRefitClientDependencyInstaller> GetRefitClientDependencyInstallers()
    {
        return typeof(IAssemblyMarkerInterface)
            .Assembly
            .DefinedTypes
            .Where(type => !type.IsAbstract && !type.IsInterface && typeof(IRefitClientDependencyInstaller).IsAssignableFrom(type))
            .Select(Activator.CreateInstance)
            .Cast<IRefitClientDependencyInstaller>();
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