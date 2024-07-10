using System.Text;
using Mohaymen.GiteaClient.Core.ApiCall.Exceptions;

namespace Mohaymen.GiteaClient.Core.ApiCall.HttpHeader;

internal static class HttpHeaderFactory
{
    private const string ApiKeyPrefix = "token ";

    public static string GetAuthorizationHeader(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            throw new InvalidApiKeyException("the api key token is null or empty. please set valid token!");
        }

        return new StringBuilder(ApiKeyPrefix).Append(token).ToString();
    }
}