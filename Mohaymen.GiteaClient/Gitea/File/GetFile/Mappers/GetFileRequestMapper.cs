using System;
using Mohaymen.GiteaClient.Gitea.File.GetFile.Commands;
using Mohaymen.GiteaClient.Gitea.File.GetRepositoryFile.Context;

namespace Mohaymen.GiteaClient.Gitea.File.GetRepositoryFile.Mappers;

internal static class GetFileRequestMapper
{
    internal static GetFileRequest ToGetRepositoryFileRequest(this GetFileCommand getFileCommand)
    {
        ArgumentNullException.ThrowIfNull(getFileCommand);

        return new GetFileRequest
        {
            ReferenceName = getFileCommand.ReferenceName
        };
    }
}