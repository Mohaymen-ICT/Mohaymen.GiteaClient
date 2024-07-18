using System.Net.Http.Headers;
using System.Text;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.AccessToken.Abstractions;
using Mohaymen.GiteaClient.IntegrationTests.Common.Models;
using Mohaymen.GiteaClient.IntegrationTests.Common.Models.Requests;
using Mohaymen.GiteaClient.IntegrationTests.Common.Models.Responses;
using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.AccessToken;

internal class AccessTokenCreator : IAccessTokenCreator
{
    public async Task<string> CreateAccessTokenAsync(string baseUrl)
    {
        using var httpClient = new HttpClient();
        var tokenAccessLevels = new List<TokenAccessLevel>()
        {
            new()
            {
                TokenAccessArea = TokenAccessArea.Admin,
                TokenAccessType = TokenAccessType.Write
            },
            new()
            {
                TokenAccessArea = TokenAccessArea.User,
                TokenAccessType = TokenAccessType.Write
            },
            new()
            {
                TokenAccessArea = TokenAccessArea.Repository,
                TokenAccessType = TokenAccessType.Write
            }
        };
        var requestBody = new CreateAccessTokenRequest
        {
            Name = GiteaTestConstants.TokenName,
            AccessLevels = GetRequestAccessLevels(tokenAccessLevels)
        };
        AddBasicAuthorizationHeader(httpClient);
        var content = CreateContent(requestBody);
        var response = await httpClient.PostAsync($"{baseUrl}/api/v1/users/{GiteaTestConstants.Username}/tokens", content);
        var serializedResponse = await response.Content.ReadAsStringAsync();
        var createAccessTokenResponse = JsonConvert.DeserializeObject<CreateAccessTokenResponse>(serializedResponse);
        return createAccessTokenResponse!.Token;
    }

    private static StringContent CreateContent(CreateAccessTokenRequest requestBody)
    {
        var content = new StringContent(JsonConvert.SerializeObject(requestBody));
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        return content;
    }

    private static void AddBasicAuthorizationHeader(HttpClient httpClient)
    {
        var basicAuthorization = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{GiteaTestConstants.Username}:{GiteaTestConstants.Password}"));
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", basicAuthorization);
    }

    private static List<string> GetRequestAccessLevels(IEnumerable<TokenAccessLevel> tokenAccessLevels)
    {
        return tokenAccessLevels.Select(x => x.ToString()).ToList();
    }
}