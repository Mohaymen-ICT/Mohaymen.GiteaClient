using System.Collections.Generic;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Commands;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Context;

namespace Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Services.Base64Encoder.Abstractions;

internal interface IBase64CommitEncoder
{
    IReadOnlyList<FileCommitCommandModel> EncodeFileContentsToBase64(IReadOnlyList<FileCommitCommandModel> fileCommitRequests);
}