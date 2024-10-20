using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.IntegrationTests.Common.Models.Responses;


internal class UserRepositoriesDto
{
    [JsonProperty("name")]
    public required string RepositoryName { get; init; }
}