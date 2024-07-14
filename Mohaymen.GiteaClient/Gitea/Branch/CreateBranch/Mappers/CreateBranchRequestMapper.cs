using Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Commands;
using Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Context;
using Riok.Mapperly.Abstractions;

namespace Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Mappers;

[Mapper]
internal static partial class CreateBranchRequestMapper
{
    public static partial CreateBranchRequest Map(CreateBranchCommand createBranchCommand);
}