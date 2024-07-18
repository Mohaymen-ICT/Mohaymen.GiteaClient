using System.Net.Mime;
using System.Text;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.GiteaUser.Abstractions;
using Mohaymen.GiteaClient.IntegrationTests.Common.Models;

namespace Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.GiteaUser;

internal class GiteaUserCreator : IGiteaUserCreator
{
    public async Task CreateGiteaUserAsync(string baseUrl)
    {
        using var httpClient = new HttpClient();
        using var multipartContent = new MultipartFormDataContent();
        multipartContent.Add(new StringContent(GiteaTestConstants.Username, Encoding.UTF8, MediaTypeNames.Text.Plain), "user_name");
        multipartContent.Add(new StringContent(GiteaTestConstants.Email, Encoding.UTF8, MediaTypeNames.Text.Plain), "email");
        multipartContent.Add(new StringContent(GiteaTestConstants.Password, Encoding.UTF8, MediaTypeNames.Text.Plain), "password");
        multipartContent.Add(new StringContent(GiteaTestConstants.Password, Encoding.UTF8, MediaTypeNames.Text.Plain), "retype");
        await httpClient.PostAsync($"{baseUrl}/user/sign_up", multipartContent);
    }
}