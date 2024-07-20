using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using Mohaymen.GiteaClient.Core.Configs;
using Mohaymen.GiteaClient.IntegrationTests.Common.Assertions.Abstractions;
using Mohaymen.GiteaClient.IntegrationTests.Common.Models;
using Mohaymen.GiteaClient.IntegrationTests.Common.Models.Responses;
using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.IntegrationTests.Common.Assertions;

internal class TestBranchChecker : ITestBranchChecker
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IOptions<GiteaApiConfiguration> _giteaOptions;

    public TestBranchChecker(IHttpClientFactory httpClientFactory,
        IOptions<GiteaApiConfiguration> giteaOptions)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _giteaOptions = giteaOptions ?? throw new ArgumentNullException(nameof(giteaOptions));
    }

    public async Task<bool> ContainsBranch(string repositoryName, string branchName)
    {
        var httpClient = _httpClientFactory.CreateClient(GiteaTestConstants.ApiClientName);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", $"{_giteaOptions.Value.PersonalAccessToken}");
        var httpResponse = await httpClient.GetAsync($"repos/{_giteaOptions.Value.RepositoriesOwner}/{repositoryName}/branches");
        var responseString = await httpResponse.Content.ReadAsStringAsync();
        var branches = JsonConvert.DeserializeObject<List<RepositoryBranchesResponse>>(responseString);
        return branches!.Select(x => x.BranchName).Contains(branchName);
    }
}