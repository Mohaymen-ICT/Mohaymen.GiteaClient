using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.Commons.Observability;
using Mohaymen.GiteaClient.Commons.Observability.Abstraction;
using Mohaymen.GiteaClient.Core.Abstractions;
using Mohaymen.GiteaClient.Core.Configs;
using Mohaymen.GiteaClient.Core.DependencyInjection.Abstractions;

namespace Mohaymen.GiteaClient.Core.DependencyInjection;

public static class DependencyInjectionExtension
{
	public static IServiceCollection AddGiteaClient(this IServiceCollection serviceCollection,
		Action<GiteaApiConfiguration, IServiceProvider> giteaOptionsAction)
	{
		serviceCollection.AddOptions<GiteaApiConfiguration>().Configure(giteaOptionsAction);
		InstallDependencies(serviceCollection);
		return serviceCollection;
	}

	public static IServiceCollection AddGiteaClient(this IServiceCollection serviceCollection,
		Action<GiteaApiConfiguration> giteaOptionsAction)
	{
		return serviceCollection.AddGiteaClient((giteaApiConfiguration, _) =>
		{
			giteaOptionsAction(giteaApiConfiguration);
		});
	}

	private static void InstallDependencies(IServiceCollection serviceCollection)
	{
		serviceCollection.AddSingleton<ITraceInstrumentation, TraceInstrumentation>();
		InstallGeneralDefinedDependencies(serviceCollection);
		InstallValidators(serviceCollection);
	}

	private static void InstallValidators(IServiceCollection serviceCollection)
	{
		serviceCollection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), ServiceLifetime.Singleton, includeInternalTypes: true);
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
