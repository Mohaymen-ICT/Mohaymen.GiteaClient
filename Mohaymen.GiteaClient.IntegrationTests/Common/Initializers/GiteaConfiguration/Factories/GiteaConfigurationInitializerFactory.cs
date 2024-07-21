using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.AccessToken;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.GiteaConfiguration.Factories.Abstractions;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.GiteaUser;

namespace Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.GiteaConfiguration.Factories;

internal static class GiteaConfigurationInitializerFactory
{
    public static IGiteaConfigurationInitializer Create()
    {
        return new GiteaConfigurationInitializer(new AccessTokenCreator(), new GiteaUserCreator());
    } 
}