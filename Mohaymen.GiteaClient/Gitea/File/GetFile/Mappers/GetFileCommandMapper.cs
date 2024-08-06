using System;
using Mohaymen.GiteaClient.Gitea.File.GetFile.Queries;
using Mohaymen.GiteaClient.Gitea.File.GetRepositoryFile.Dtos;

namespace Mohaymen.GiteaClient.Gitea.File.GetFile.Mappers;

internal static class GetFileCommandMapper
{
    internal static GetFileMetadataQuery ToGetFileCommand(this GetFileMetadataQueryDto getFileMetadataQueryDto)
    {
        ArgumentNullException.ThrowIfNull(getFileMetadataQueryDto);

        return new GetFileMetadataQuery
        {
            RepositoryName = getFileMetadataQueryDto.RepositoryName,
            FilePath = getFileMetadataQueryDto.FilePath,
            ReferenceName = getFileMetadataQueryDto.ReferenceName
        };
    }
}