using Microsoft.Extensions.DependencyInjection;
using Mohaymen.GiteaClient.Core.DependencyInjection.Abstractions;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.AccessToken;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.AccessToken.Abstractions;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.GiteaConfiguration.Factories;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.GiteaConfiguration.Factories.Abstractions;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.GiteaUser;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.GiteaUser.Abstractions;

namespace Mohaymen.GiteaClient.IntegrationTests.Common.DependencyInjection;

public class IntegrationTestsDependencyInstaller : IDependencyInstaller
{
    public void Install(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IAccessTokenCreator, AccessTokenCreator>();
        serviceCollection.AddSingleton<IGiteaConfigurationInitializer, GiteaConfigurationInitializer>();
        serviceCollection.AddSingleton<IGiteaUserCreator, GiteaUserCreator>();
    }
}