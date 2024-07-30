using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Commands;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Context;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Services.Base64Encoder.Abstractions;

namespace Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Services.Base64Encoder;

internal class Base64CommitEncoder : IBase64CommitEncoder
{
    public IReadOnlyList<FileCommitCommandModel> EncodeFileContentsToBase64(IReadOnlyList<FileCommitCommandModel> fileCommitRequests)
    {
        return fileCommitRequests.Select(fileCommitRequest => fileCommitRequest with
        {
            Content = Convert.ToBase64String(Encoding.UTF8.GetBytes(fileCommitRequest.Content))
        }).ToList();
    }
}