using Mohaymen.GiteaClient.Core.Configs;

namespace Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.GiteaConfiguration.Factories.Abstractions;

internal interface IGiteaConfigurationInitializer
{
    Task<GiteaApiConfiguration> CreateGiteaApiConfiguration(string giteaBaseUrl);
}