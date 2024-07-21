using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.IntegrationTests.Common.Models.Responses;

internal class CreateAccessTokenResponse
{
    [JsonProperty("sha1")]
    public required string Token { get; init; }
}