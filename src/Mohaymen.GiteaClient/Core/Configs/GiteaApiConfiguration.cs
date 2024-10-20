using System;

namespace Mohaymen.GiteaClient.Core.Configs;

public sealed class GiteaApiConfiguration
{
    /// <summary>
    /// gitea server url
    /// </summary>
    public required string BaseUrl { get; set; }
    /// <summary>
    /// personal access token from gitea account
    /// </summary>
    public required string PersonalAccessToken { get; set; }
    /// <summary>
    /// http connection timeout
    /// </summary>
    public TimeSpan ApiConnectionTimeout { get; init; } = TimeSpan.FromHours(1);
    
    /// <summary>
    /// owner of repository username
    /// </summary>
    public required string RepositoriesOwner { get; set; }
}