using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Commands;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Context;
using Riok.Mapperly.Abstractions;

namespace Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Mappers;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
internal static partial class FileCommitRequestMapper
{
    [MapProperty(nameof(FileCommitCommand.CommitActionCommand), nameof(FileCommitRequest.CommitAction))]
    public static partial FileCommitRequest Map(FileCommitCommand fileCommitCommand);
}