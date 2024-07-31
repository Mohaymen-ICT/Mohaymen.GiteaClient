using System;
using Mohaymen.GiteaClient.Gitea.File.GetFile.Commands;
using Mohaymen.GiteaClient.Gitea.File.GetRepositoryFile.Context;

namespace Mohaymen.GiteaClient.Gitea.File.GetFile.Mappers;

internal static class GetFileRequestMapper
{
    internal static GetFileRequest ToGetFileRequest(this GetFileCommand getFileCommand)
    {
        ArgumentNullException.ThrowIfNull(getFileCommand);

        return new GetFileRequest
        {
            ReferenceName = getFileCommand.ReferenceName
        };
    }
}