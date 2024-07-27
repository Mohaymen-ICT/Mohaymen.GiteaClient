using System;
using Mohaymen.GiteaClient.Gitea.File.Common.Encoding.Abstraction;

namespace Mohaymen.GiteaClient.Gitea.File.Common.Encoding;

public class ContentEncoder : IContentEncoder
{
    public string Base64Encode(string content)
    {
        var contentBytes = System.Text.Encoding.UTF8.GetBytes(content);
        return Convert.ToBase64String(contentBytes);
    }
}