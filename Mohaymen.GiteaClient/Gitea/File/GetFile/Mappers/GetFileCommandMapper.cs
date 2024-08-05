﻿using System;
using Mohaymen.GiteaClient.Gitea.File.GetFile.Commands;
using Mohaymen.GiteaClient.Gitea.File.GetRepositoryFile.Dtos;

namespace Mohaymen.GiteaClient.Gitea.File.GetFile.Mappers;

internal static class GetFileCommandMapper
{
    internal static GetFileCommand ToGetFileCommand(this GetFileCommandDto getFileCommandDto)
    {
        ArgumentNullException.ThrowIfNull(getFileCommandDto);

        return new GetFileCommand
        {
            RepositoryName = getFileCommandDto.RepositoryName,
            FilePath = getFileCommandDto.FilePath,
            ReferenceName = getFileCommandDto.ReferenceName
        };
    }
}