using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.IntegrationTests.Common.Models.Requests;

internal class CreateAccessTokenRequest
{
    [JsonProperty("name")]
    public required string Name { get; init; }
    
    [JsonProperty("scopes")]
    public required List<string> AccessLevels { get; set;}
}


internal class TokenAccessLevel
{
    public required TokenAccessArea TokenAccessArea { get; init; }
    public required TokenAccessType TokenAccessType { get; init; }
    
    public override string ToString()
    {
        return $"{TokenAccessType.ToString().ToLowerInvariant()}:{TokenAccessArea.ToString().ToLowerInvariant()}";
    }
}

internal enum TokenAccessArea
{
    Activitypub,
    Admin,
    Issue,
    Misc,
    Notification,
    Organization,
    Package,
    Repository,
    User
}

internal enum TokenAccessType
{
    Write,
    Read
}