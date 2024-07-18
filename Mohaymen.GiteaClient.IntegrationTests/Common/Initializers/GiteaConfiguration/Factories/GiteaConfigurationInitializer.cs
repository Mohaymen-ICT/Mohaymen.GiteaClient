using Mohaymen.GiteaClient.Core.Configs;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.AccessToken.Abstractions;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.GiteaConfiguration.Factories.Abstractions;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.GiteaUser.Abstractions;
using Mohaymen.GiteaClient.IntegrationTests.Common.Models;

namespace Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.GiteaConfiguration.Factories;

internal class GiteaConfigurationInitializer : IGiteaConfigurationInitializer
{
    private readonly IAccessTokenCreator _accessTokenCreator;
    private readonly IGiteaUserCreator _giteaUserCreator;

    public GiteaConfigurationInitializer(IAccessTokenCreator accessTokenCreator, IGiteaUserCreator giteaUserCreator)
    {
        _accessTokenCreator = accessTokenCreator ?? throw new ArgumentNullException(nameof(accessTokenCreator));
        _giteaUserCreator = giteaUserCreator ?? throw new ArgumentNullException(nameof(giteaUserCreator));
    }

    public async Task<GiteaApiConfiguration> CreateGiteaApiConfiguration(string giteaBaseUrl)
    {
        await _giteaUserCreator.CreateGiteaUserAsync(giteaBaseUrl);
        var token = await _accessTokenCreator.CreateAccessTokenAsync(giteaBaseUrl);
        return new GiteaApiConfiguration
        {
            BaseUrl = giteaBaseUrl,
            PersonalAccessToken = token,
            RepositoriesOwner = GiteaTestConstants.Username
        };
    }
    
}