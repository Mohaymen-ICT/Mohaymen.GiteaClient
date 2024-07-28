using System;
using System.Collections.Generic;
using System.Text;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Context;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Services.Base64Encoder.Abstractions;

namespace Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Services.Base64Encoder;

internal class Base64CommitEncoder : IBase64CommitEncoder
{
    public void EncodeFileContentsToBase64(IReadOnlyList<FileCommitRequest> fileCommitRequests)
    {
        foreach (var fileCommitRequest in fileCommitRequests)
        {
            var base64StringContent = Convert.ToBase64String(Encoding.UTF8.GetBytes(fileCommitRequest.Content));
            fileCommitRequest.Content = base64StringContent;
        }
    }
}