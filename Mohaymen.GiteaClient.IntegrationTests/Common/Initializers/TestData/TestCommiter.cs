using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using Mohaymen.GiteaClient.Core.Configs;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData.Abstractions;
using Mohaymen.GiteaClient.IntegrationTests.Common.Models;
using Mohaymen.GiteaClient.IntegrationTests.Common.Models.Requests;
using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData;

internal class TestCommiter : ITestCommiter
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IOptions<GiteaApiConfiguration> _giteaOptions;

    public TestCommiter(IHttpClientFactory httpClientFactory, IOptions<GiteaApiConfiguration> giteaOptions)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _giteaOptions = giteaOptions ?? throw new ArgumentNullException(nameof(giteaOptions));
    }

    public async Task CreateFileAsync(string repositoryName,
        string branchName,
        string filePath,
        string commitMessage,
        CancellationToken cancellationToken)
    {
        var httpClient = _httpClientFactory.CreateClient(GiteaTestConstants.ApiClientName);
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("token", $"{_giteaOptions.Value.PersonalAccessToken}");
        var createFileRequest = new CreateFileRequest
        {
            Content = Convert.ToBase64String("sample test content"u8.ToArray()),
            CommitMessage = commitMessage,
            BranchName = branchName
        };
        var jsonContent = new StringContent(JsonConvert.SerializeObject(createFileRequest));
        jsonContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        var resykt = await httpClient.PostAsync($"repos/{_giteaOptions.Value.RepositoriesOwner}/{repositoryName}/contents/{filePath}", jsonContent, cancellationToken);
    }
}