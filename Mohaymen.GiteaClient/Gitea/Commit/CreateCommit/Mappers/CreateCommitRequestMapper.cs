using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Commands;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Context;
using Riok.Mapperly.Abstractions;

namespace Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Mappers;

[Mapper]
[UseStaticMapper(typeof(FileCommitRequestMapper))]
internal static partial class CreateCommitRequestMapper
{
    [MapProperty(nameof(CreateCommitCommand.FileCommitCommands), nameof(CreateCommitRequest.FileCommitRequests))]
    public static partial CreateCommitRequest MapToRequest(this CreateCommitCommand createCommitCommand);
}