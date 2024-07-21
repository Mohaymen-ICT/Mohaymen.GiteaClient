using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using Mohaymen.GiteaClient.Core.Configs;
using Mohaymen.GiteaClient.IntegrationTests.Common.Assertions.Abstractions;
using Mohaymen.GiteaClient.IntegrationTests.Common.Models;
using Mohaymen.GiteaClient.IntegrationTests.Common.Models.Responses;
using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.IntegrationTests.Common.Assertions;

internal class TestFileChecker : ITestFileChecker
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IOptions<GiteaApiConfiguration> _options;

    public TestFileChecker(IHttpClientFactory httpClientFactory, IOptions<GiteaApiConfiguration> options)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    public async Task<bool> ContainsFileAsync(string repositoryName, string fileName, CancellationToken cancellationToken)
    {
        var httpClient = _httpClientFactory.CreateClient(GiteaTestConstants.ApiClientName);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", $"{_options.Value.PersonalAccessToken}");
        var httpResponse = await httpClient.GetAsync($"repos/{_options.Value.RepositoriesOwner}/{repositoryName}/contents", cancellationToken);
        var serializedResponse = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
        var filesList = JsonConvert.DeserializeObject<List<FileMetadataResponse>>(serializedResponse);
        return filesList!.Select(x => x.FileName).Contains(fileName);
    }

    public async Task<bool> HasFileContent(string repositoryName, string filePath, string content, CancellationToken cancellationToken)
    {
        var httpClient = _httpClientFactory.CreateClient(GiteaTestConstants.ApiClientName);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", $"{_options.Value.PersonalAccessToken}");
        var httpResponse = await httpClient.GetAsync($"repos/{_options.Value.RepositoriesOwner}/{repositoryName}/contents/{filePath}", cancellationToken);
        var serializedResponse = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
        var filesList = JsonConvert.DeserializeObject<List<FileMetadataResponse>>(serializedResponse);
        var file = filesList!.First(x => x.FilePath == filePath);
        return file.Content == content;
    }
}