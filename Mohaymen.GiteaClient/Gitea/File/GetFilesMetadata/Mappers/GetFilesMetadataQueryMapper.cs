using System;
using Mohaymen.GiteaClient.Gitea.File.GetFilesMetadata.Dto;
using Mohaymen.GiteaClient.Gitea.File.GetFilesMetadata.Queries;

namespace Mohaymen.GiteaClient.Gitea.File.GetFilesMetadata.Mappers;

internal static class GetFilesMetadataQueryMapper
{
    public static GetFilesMetadataQuery Map(GetFilesMetadataQueryDto getFilesMetadataQueryDto)
    {
        ArgumentNullException.ThrowIfNull(getFilesMetadataQueryDto);

        return new GetFilesMetadataQuery
        {
            RepositoryName = getFilesMetadataQueryDto.RepositoryName,
            BranchName = getFilesMetadataQueryDto.BranchName
        };
    }
}