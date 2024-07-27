namespace Mohaymen.GiteaClient.Gitea.File.Common.Encoding.Abstraction;

public interface IContentDecoder
{
    string Base64Decode(string encodedContent);
}