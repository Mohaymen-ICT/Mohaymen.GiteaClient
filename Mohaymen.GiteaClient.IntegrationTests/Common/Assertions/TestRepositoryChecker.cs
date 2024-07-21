using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using Mohaymen.GiteaClient.Core.Configs;
using Mohaymen.GiteaClient.IntegrationTests.Common.Assertions.Abstractions;
using Mohaymen.GiteaClient.IntegrationTests.Common.Models;
using Mohaymen.GiteaClient.IntegrationTests.Common.Models.Responses;
using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.IntegrationTests.Common.Assertions;

internal class TestRepositoryChecker : ITestRepositoryChecker
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IOptions<GiteaApiConfiguration> _giteaOptions;

    public TestRepositoryChecker(IHttpClientFactory httpClientFactory,
        IOptions<GiteaApiConfiguration> giteaOptions)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _giteaOptions = giteaOptions ?? throw new ArgumentNullException(nameof(giteaOptions));
    }

    public async Task<bool> ContainsRepositoryAsync(string repositoryName, CancellationToken cancellationToken)
    {
        var httpClient = _httpClientFactory.CreateClient(GiteaTestConstants.ApiClientName);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", $"{_giteaOptions.Value.PersonalAccessToken}");
        var httpResponse = await httpClient.GetAsync($"users/{_giteaOptions.Value.RepositoriesOwner}/repos", cancellationToken);
        var responseString = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
        var repositoryDtos = JsonConvert.DeserializeObject<List<UserRepositoriesDto>>(responseString);
        return repositoryDtos!.Select(x => x.RepositoryName).Contains(repositoryName);
    }
}