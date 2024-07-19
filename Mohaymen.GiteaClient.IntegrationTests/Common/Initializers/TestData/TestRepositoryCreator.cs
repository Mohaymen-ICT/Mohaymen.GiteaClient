using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using Mohaymen.GiteaClient.Core.Configs;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData.Abstractions;
using Mohaymen.GiteaClient.IntegrationTests.Common.Models.Requests;
using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData;

internal class TestRepositoryCreator : ITestRepositoryCreator
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IOptions<GiteaApiConfiguration> _giteaOptions;
    
    public TestRepositoryCreator(IHttpClientFactory httpClientFactory,
        IOptions<GiteaApiConfiguration> giteaOptions)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _giteaOptions = giteaOptions ?? throw new ArgumentNullException(nameof(giteaOptions));
    }


    public async Task CreateRepository(string repositoryName)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var createRepositoryRequest = new CreateIntegrationTestRepositoryRequest
        {
            DefaultBranch = "main",
            Name = repositoryName,
            Readme = "Default",
            AutoInit = true,
            IsPrivateBranch = true
        };
        var jsonContent = new StringContent(JsonConvert.SerializeObject(createRepositoryRequest));
        jsonContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", $"{_giteaOptions.Value.PersonalAccessToken}");
        await httpClient.PostAsync(new Uri($"{_giteaOptions.Value.BaseUrl}/api/v1/user/repos"), jsonContent);
    }
}