using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.APICall.Domain;
using Mohaymen.GiteaClient.Common.Abstractions;
using Mohaymen.GiteaClient.Common.DependencyInjection.Abstractions;

namespace Mohaymen.GiteaClient.Common.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddGitClientServices(this IServiceCollection serviceCollection, Action<GiteaApiConfiguration> giteaOptions)
    {
        var dependencyInstallers = GetDependencyInstallers();

        foreach (var dependencyInstaller in dependencyInstallers)
        {
            dependencyInstaller.Install(serviceCollection);
        }

        return serviceCollection;
    }

    private static IServiceCollection AddCommonDependencies(IServiceCollection serviceCollection, Action<GiteaApiConfiguration> giteaOptions)
    {
        serviceCollection.Configure(giteaOptions);
        serviceCollection.AddSingleton<GiteaApiConfiguration>();
        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        serviceCollection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return serviceCollection;
    }

    private static IEnumerable<IDependencyInstaller> GetDependencyInstallers()
    {
        return typeof(IAssemblyMarkerInterface)
            .Assembly
            .DefinedTypes
            .Where(type => !type.IsAbstract && !type.IsInterface && typeof(IDependencyInstaller).IsAssignableFrom(type))
            .Select(Activator.CreateInstance)
            .Cast<IDependencyInstaller>();
    }
}