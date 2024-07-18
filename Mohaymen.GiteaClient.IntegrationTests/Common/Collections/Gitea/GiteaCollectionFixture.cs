using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.Core.Configs;
using Mohaymen.GiteaClient.Core.DependencyInjection;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.GiteaConfiguration.Factories;
using Mohaymen.GiteaClient.IntegrationTests.Common.Models;

namespace Mohaymen.GiteaClient.IntegrationTests.Common.Collections.Gitea;

public class GiteaCollectionFixture : IAsyncLifetime
{
    public IServiceProvider ServiceProvider { get; private set; } = null!;
    public CancellationToken CancellationToken { get; private set; }
    private IContainer _container;
    

    public async Task InitializeAsync()
    {
        var cts = new CancellationTokenSource();
        CancellationToken = cts.Token;
        await StartContainerAsync();
        var baseUrl = GetBaseUrl();
        var giteaConfigurationInitializer = GiteaConfigurationInitializerFactory.Create();
        var giteaApiConfiguration = await giteaConfigurationInitializer
            .CreateGiteaApiConfiguration(baseUrl)
            .ConfigureAwait(false);
        BuildDependencies(giteaApiConfiguration);
    }

    private void BuildDependencies(GiteaApiConfiguration giteaApiConfiguration)
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddGiteaClient(configuration =>
        {
            configuration.BaseUrl = giteaApiConfiguration.BaseUrl;
            configuration.RepositoriesOwner = giteaApiConfiguration.RepositoriesOwner;
            configuration.PersonalAccessToken = giteaApiConfiguration.PersonalAccessToken;
        });
        ServiceProvider = serviceCollection.BuildServiceProvider();
    }

    private string GetBaseUrl()
    {
        var hostPort = _container.GetMappedPublicPort(GiteaTestConstants.PortNumber);
        return $"http://{_container.Hostname}:{hostPort}";
    }

    private async Task StartContainerAsync()
    {
        _container = new ContainerBuilder()
            .WithImage(GiteaTestConstants.ImageName)
            .WithPortBinding(GiteaTestConstants.PortNumber, true)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilHttpRequestIsSucceeded(r => r.ForPort(3000)))
            .WithEnvironment(new Dictionary<string, string>
            {
                { "USER_UID", "1000" },
                { "USER_GID", "1000" },
                { "GITEA__security__INSTALL_LOCK", "true" }
            })
            .Build();
        await _container.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await _container.DisposeAsync();
    }
}