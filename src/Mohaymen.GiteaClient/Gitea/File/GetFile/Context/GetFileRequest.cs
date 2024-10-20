using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.Gitea.File.GetRepositoryFile.Context;

internal class GetFileRequest
{
    [JsonProperty("ref")]
    public string? ReferenceName { get; init; }
}