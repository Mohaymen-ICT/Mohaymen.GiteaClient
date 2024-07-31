namespace Mohaymen.GiteaClient.Gitea.File.Common.Encoding.Abstraction;

public interface IContentEncoder
{
    string Base64Encode(string content);
}