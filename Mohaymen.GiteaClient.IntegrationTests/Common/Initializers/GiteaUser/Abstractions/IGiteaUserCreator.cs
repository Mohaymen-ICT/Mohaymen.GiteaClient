namespace Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.GiteaUser.Abstractions;

internal interface IGiteaUserCreator
{
    Task CreateGiteaUserAsync(string baseUrl);
}