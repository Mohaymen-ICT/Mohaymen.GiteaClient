using Mohaymen.GiteaClient.Gitea.File.Common.Encoding.Abstraction;

namespace Mohaymen.GiteaClient.Gitea.File.Common.Encoding;

public class ContentDecoder : IContentDecoder
{
    public string Base64Decode(string encodedContent)
    {
        var base64EncodedBytes = System.Convert.FromBase64String(encodedContent);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }
}