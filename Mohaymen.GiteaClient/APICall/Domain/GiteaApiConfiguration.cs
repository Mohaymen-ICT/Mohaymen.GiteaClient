using System;

namespace Mohaymen.GitClient.APICall.Domain;

public sealed class GiteaApiConfiguration
{
    internal string BaseUrl { get;}
    internal string PersonalAccessToken { get; }
    internal TimeSpan ApiConnectionTimeout { get; } = TimeSpan.FromHours(1);
    internal string RepositoriesOwner { get; }
    
    /// <summary>
    ///  gitea general configuration
    /// </summary>
    /// <param name="baseUrl">the url of the girea server</param>
    /// <param name="personalAccessToken">the gitea personal access token</param>
    /// <param name="repositoriesOwner">the username of the owner of all repositories</param>
    /// <exception cref="ArgumentNullException"></exception>
    public GiteaApiConfiguration(string baseUrl,
        string personalAccessToken,
        string repositoriesOwner)
    {
        BaseUrl = baseUrl ?? throw new ArgumentNullException(nameof(baseUrl));
        PersonalAccessToken = personalAccessToken ?? throw new ArgumentNullException(nameof(personalAccessToken));
        RepositoriesOwner = repositoriesOwner ?? throw new ArgumentNullException(nameof(repositoriesOwner));
    }
    
    /// <summary>
    /// gitea general configuration
    /// </summary>
    /// <param name="baseUrl">the url of the girea server</param>
    /// <param name="personalAccessToken">the gitea personal access token</param>
    /// <param name="repositoriesOwner">the username of the owner of all repositories</param>
    /// <param name="apiConnectionTimeout">the http connection timeout</param>
    /// <exception cref="ArgumentNullException"></exception>
    public GiteaApiConfiguration(string baseUrl,
        string personalAccessToken,
        string repositoriesOwner,
        TimeSpan apiConnectionTimeout) : this(baseUrl, personalAccessToken, repositoriesOwner)
    {
        ApiConnectionTimeout = apiConnectionTimeout;
    }
    
}