using System;

namespace Mohaymen.GitClient.APICall.Domain;

public class GiteaApiConfiguration
{
    internal string BaseUrl { get;}
    internal string PersonalAccessToken { get; }
    internal TimeSpan ApiConnectionTimeout { get; } = TimeSpan.FromHours(1);
    
    public GiteaApiConfiguration(string baseUrl,
        string personalAccessToken)
    {
        BaseUrl = baseUrl ?? throw new ArgumentNullException(nameof(baseUrl));
        PersonalAccessToken = personalAccessToken ?? throw new ArgumentNullException(nameof(personalAccessToken));
    }

    public GiteaApiConfiguration(string baseUrl,
        string personalAccessToken,
        TimeSpan apiConnectionTimeout) : this(baseUrl, personalAccessToken)
    {
        ApiConnectionTimeout = apiConnectionTimeout;
    }
    
}