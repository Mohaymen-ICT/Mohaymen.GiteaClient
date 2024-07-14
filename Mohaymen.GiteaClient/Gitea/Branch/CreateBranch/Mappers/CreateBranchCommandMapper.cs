using Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Commands;
using Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Dtos;
using Riok.Mapperly.Abstractions;

namespace Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Mappers;

[Mapper]
internal static partial class CreateBranchCommandMapper
{
    public static partial CreateBranchCommand Map(CreateBranchCommandDto createBranchCommandDto);
}