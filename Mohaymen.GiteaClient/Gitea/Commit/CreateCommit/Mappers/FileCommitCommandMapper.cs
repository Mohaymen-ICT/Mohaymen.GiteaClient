﻿using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Commands;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Dtos.Request;
using Riok.Mapperly.Abstractions;

namespace Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Mappers;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
internal static partial class FileCommitCommandMapper
{
    [MapProperty(nameof(FileCommitDto.CommitActionDto), nameof(FileCommitCommandModel.CommitActionCommand))]
    public static partial FileCommitCommandModel Map(FileCommitDto fileCommitDto);
}