using System;
using Mohaymen.GiteaClient.Gitea.File.GetFile.Queries;
using Mohaymen.GiteaClient.Gitea.File.GetRepositoryFile.Context;

namespace Mohaymen.GiteaClient.Gitea.File.GetFile.Mappers;

internal static class GetFileRequestMapper
{
    internal static GetFileRequest ToGetFileRequest(this GetFileMetadataQuery getFileMetadataQuery)
    {
        ArgumentNullException.ThrowIfNull(getFileMetadataQuery);

        return new GetFileRequest
        {
            ReferenceName = getFileMetadataQuery.ReferenceName
        };
    }
}