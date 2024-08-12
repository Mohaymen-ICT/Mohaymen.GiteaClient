using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.Core.Configs;
using Mohaymen.GiteaClient.Core.DependencyInjection;
using Mohaymen.GiteaClient.IntegrationTests.Common.DependencyInjection;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.GiteaConfiguration.Factories;
using Mohaymen.GiteaClient.IntegrationTests.Common.Models;

namespace Mohaymen.GiteaClient.IntegrationTests.Common.Collections.Gitea;

public class GiteaCollectionFixture : IAsyncLifetime
{
    public IServiceProvider ServiceProvider { get; private set; } = null!;
    public CancellationToken CancellationToken { get; private set; }
    private IContainer _container = null!;
    

    public async Task InitializeAsync()
    {
        var cancellationTokenSource = new CancellationTokenSource();
        CancellationToken = cancellationTokenSource.Token;
        _container = await StartContainerAsync();
        var baseUrl = GetBaseUrl(_container);
        var giteaConfigurationInitializer = GiteaConfigurationInitializerFactory.Create();
        var giteaApiConfiguration = await giteaConfigurationInitializer
            .CreateGiteaApiConfiguration(baseUrl)
            .ConfigureAwait(false);
        BuildDependencies(giteaApiConfiguration, baseUrl);
    }

    private void BuildDependencies(GiteaApiConfiguration giteaApiConfiguration, string baseUrl)
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddGiteaIntegrationTestsServices($"{baseUrl}/api/v1/");
        serviceCollection.AddGiteaClient(configuration =>
        {
            configuration.BaseUrl = giteaApiConfiguration.BaseUrl;
            configuration.RepositoriesOwner = giteaApiConfiguration.RepositoriesOwner;
            configuration.PersonalAccessToken = giteaApiConfiguration.PersonalAccessToken;
        });
        ServiceProvider = serviceCollection.BuildServiceProvider();
    }

    private static string GetBaseUrl(IContainer container)
    {
        var hostPort = container.GetMappedPublicPort(GiteaTestConstants.PortNumber);
        return $"http://{container.Hostname}:{hostPort}";
    }

    private static async Task<IContainer> StartContainerAsync()
    {
        var container = new ContainerBuilder()
            .WithImage(GiteaTestConstants.ImageName)
            .WithPortBinding(GiteaTestConstants.PortNumber, true)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilHttpRequestIsSucceeded(r => r.ForPort(GiteaTestConstants.PortNumber)))
            .WithEnvironment(new Dictionary<string, string>
            {
                { "USER_UID", "1000" },
                { "USER_GID", "1000" },
                { "GITEA__security__INSTALL_LOCK", "true" }
            })
            .Build();
        await container.StartAsync();
        return container;
    }

    public async Task DisposeAsync()
    {
        await _container.DisposeAsync();
    }
}