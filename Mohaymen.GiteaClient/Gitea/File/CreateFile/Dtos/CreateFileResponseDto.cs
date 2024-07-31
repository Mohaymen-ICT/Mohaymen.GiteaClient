using Mohaymen.GiteaClient.Gitea.File.CreateFile.Models;
using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.Gitea.File.CreateFile.Dtos;

public record CreateFileResponseDto
{
    [JsonProperty("content")]
    public required Content Content { get; init; }
}