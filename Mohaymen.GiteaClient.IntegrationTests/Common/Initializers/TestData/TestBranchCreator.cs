using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using Mohaymen.GiteaClient.Core.Configs;
using Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Context;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData.Abstractions;
using Mohaymen.GiteaClient.IntegrationTests.Common.Models;
using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData;

public class TestBranchCreator : ITestBranchCreator
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IOptions<GiteaApiConfiguration> _giteaOptions;
    
    public TestBranchCreator(IHttpClientFactory httpClientFactory,
        IOptions<GiteaApiConfiguration> giteaOptions)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _giteaOptions = giteaOptions ?? throw new ArgumentNullException(nameof(giteaOptions));
    }
    
    public async Task CreateBranch(string repositoryName, string branchName)
    {
        var httpClient = _httpClientFactory.CreateClient(GiteaTestConstants.ApiClientName);
        var owner = GiteaTestConstants.Username;
        var createBranchRequest = new CreateBranchRequest
        {
            NewBranchName = branchName,
            OldReferenceName = GiteaTestConstants.DefaultBranch
        };
        var jsonContent = new StringContent(JsonConvert.SerializeObject(createBranchRequest));
        jsonContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", _giteaOptions.Value.PersonalAccessToken);
        await httpClient.PostAsync($"repos/{owner}/{repositoryName}/branches", jsonContent);
    }
}