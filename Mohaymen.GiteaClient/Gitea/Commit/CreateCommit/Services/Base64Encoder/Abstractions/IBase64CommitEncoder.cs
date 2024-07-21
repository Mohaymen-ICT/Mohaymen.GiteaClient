using System.Collections.Generic;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Context;

namespace Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Services.Base64Encoder.Abstractions;

internal interface IBase64CommitEncoder
{
    void EncodeFileContentsToBase64(List<FileCommitRequest> fileCommitRequests);
}