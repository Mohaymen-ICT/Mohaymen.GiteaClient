using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using Mohaymen.GiteaClient.Core.Configs;
using Mohaymen.GiteaClient.Gitea.Commit.GetBranchCommits.Dtos;
using Mohaymen.GiteaClient.IntegrationTests.Common.Assertions.Abstractions;
using Mohaymen.GiteaClient.IntegrationTests.Common.Models;
using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.IntegrationTests.Common.Assertions;

public class TestCommitChecker : ITestCommitChecker
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IOptions<GiteaApiConfiguration> _options;

    public TestCommitChecker(IHttpClientFactory httpClientFactory, IOptions<GiteaApiConfiguration> options)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    public async Task<bool> ContainsCommitWithShaAsync(string repositoryName,
        string branchName,
        string commitSha,
        CancellationToken cancellationToken)
    {
        var httpClient = _httpClientFactory.CreateClient(GiteaTestConstants.ApiClientName);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", $"{_options.Value.PersonalAccessToken}");
        var httpResponse = await httpClient.GetAsync($"repos/{_options.Value.RepositoriesOwner}/{repositoryName}/commits?sha={branchName}", cancellationToken);
        var serializedResponse = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
        var commitsList = JsonConvert.DeserializeObject<List<LoadBranchCommitsResponseDto>>(serializedResponse);
        return commitsList!.Select(x => x.CommitSha).Contains(commitSha);
    }
}