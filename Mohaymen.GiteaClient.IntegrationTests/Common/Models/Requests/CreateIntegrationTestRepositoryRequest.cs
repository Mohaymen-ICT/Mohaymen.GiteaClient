using Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Context;
using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.IntegrationTests.Common.Models.Requests;

internal class CreateIntegrationTestRepositoryRequest : CreateRepositoryRequest
{
    [JsonProperty("readme")]
    public required string Readme { get; init; }
}