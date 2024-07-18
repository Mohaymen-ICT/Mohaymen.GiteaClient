using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Mohaymen.GiteaClient.Core.Configs;
using Mohaymen.GiteaClient.IntegrationTests.Common.Collections.Gitea;
using Mohaymen.GiteaClient.IntegrationTests.Common.Models.Requests;
using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.IntegrationTests.Gitea.Branch;

public class BranchTestsClassFixture : IAsyncLifetime
{
    public const string RepositoryName = "BranchUseCaseRepository";
    
    private readonly GiteaCollectionFixture _giteaCollectionFixture;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IOptions<GiteaApiConfiguration> _giteaOptions;
    
    public BranchTestsClassFixture(GiteaCollectionFixture giteaCollectionFixture)
    {
        _giteaCollectionFixture = giteaCollectionFixture ?? throw new ArgumentNullException(nameof(giteaCollectionFixture));
        _httpClientFactory = _giteaCollectionFixture.ServiceProvider.GetRequiredService<IHttpClientFactory>();
        _giteaOptions = _giteaCollectionFixture.ServiceProvider.GetRequiredService<IOptions<GiteaApiConfiguration>>();
    }

    public async Task InitializeAsync()
    {
        var httpClient = _httpClientFactory.CreateClient();
        var createRepositoryRequest = new CreateIntegrationTestRepositoryRequest
        {
            DefaultBranch = "main",
            Name = RepositoryName,
            Readme = "some readme to add content to the repo"
        };
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", $"{_giteaOptions.Value.PersonalAccessToken}");
        await httpClient.PostAsJsonAsync(new Uri($"{_giteaOptions.Value.BaseUrl}/api/v1/user/repos"), createRepositoryRequest);
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
}