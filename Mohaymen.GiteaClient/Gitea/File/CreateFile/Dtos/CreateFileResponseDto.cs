using Mohaymen.GiteaClient.Gitea.File.CreateFile.Models;
using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.Gitea.File.CreateFile.Dtos;

public class CreateFileResponseDto
{
    [JsonProperty("content")]
    public required Content Content { get; init; }
}