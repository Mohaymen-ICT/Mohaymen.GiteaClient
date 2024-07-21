using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.IntegrationTests.Common.Models.Requests;
internal class CreateAccessTokenRequest
{
    [JsonProperty("name")]
    public required string Name { get; init; }
    
    [JsonProperty("scopes")]
    public required List<string> AccessLevels { get; set;}
}
