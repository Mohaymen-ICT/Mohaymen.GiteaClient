﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GitClient.Common.Abstractions;
using Mohaymen.GitClient.Common.DependencyInjection.Abstractions;

namespace Mohaymen.GitClient.Common.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddGitClientCommonDependencies(this IServiceCollection serviceCollection)
    {
        var dependencyInstallers = GetDependencyInstallers();

        foreach (var dependencyInstaller in dependencyInstallers)
        {
            dependencyInstaller.Install(serviceCollection);
        }

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