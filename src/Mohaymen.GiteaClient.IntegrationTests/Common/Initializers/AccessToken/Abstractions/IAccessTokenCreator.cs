namespace Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.AccessToken.Abstractions;

internal interface IAccessTokenCreator
{
    Task<string> CreateAccessTokenAsync(string baseUrl);
}