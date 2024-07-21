using System.Collections.Generic;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Context;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Services.Base64Encoder.Abstractions;

namespace Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Services.Base64Encoder;

internal class Base64CommitEncoder : IBase64CommitEncoder
{
    public void EncodeFileContentsToBase64(List<FileCommitRequest> fileCommitRequests)
    {
        foreach (var fileCommitRequest in fileCommitRequests)
        {
            
        }
    }
}