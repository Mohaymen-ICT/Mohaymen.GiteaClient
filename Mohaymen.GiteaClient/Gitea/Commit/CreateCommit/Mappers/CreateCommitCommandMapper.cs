using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Commands;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Dtos.Request;
using Riok.Mapperly.Abstractions;

namespace Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Mappers;

[Mapper]
[UseStaticMapper(typeof(FileCommitCommandMapper))]
internal static partial class CreateCommitCommandMapper
{
    [MapProperty(nameof(CreateCommitCommandDto.FileDtos), nameof(CreateCommitCommand.FileCommitCommands))]
    public static partial CreateCommitCommand MapToCommand(this CreateCommitCommandDto commitCommandDto);
}