using System;
using Mohaymen.GiteaClient.Gitea.Branch.GetBranchList.Commands;
using Mohaymen.GiteaClient.Gitea.Branch.GetBranchList.Dtos;

namespace Mohaymen.GiteaClient.Gitea.Branch.GetBranchList.Mappers;

internal static class GetBranchListCommandMapper
{
    internal static GetBranchListCommand ToGetBranchListCommand(this GetBranchListCommandDto getBranchListCommandDto)
    {
        if (getBranchListCommandDto is null)
        {
            throw new ArgumentNullException(nameof(getBranchListCommandDto));
        }

        return new GetBranchListCommand
        {
            RepositoryName = getBranchListCommandDto.RepositoryName,
        };
    }
}