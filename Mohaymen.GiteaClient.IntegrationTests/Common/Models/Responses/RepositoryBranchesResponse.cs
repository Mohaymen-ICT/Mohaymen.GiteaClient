using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.IntegrationTests.Common.Models.Responses;

internal class RepositoryBranchesResponse
{
    [JsonProperty("name")]
    public required string BranchName { get; init; }
}