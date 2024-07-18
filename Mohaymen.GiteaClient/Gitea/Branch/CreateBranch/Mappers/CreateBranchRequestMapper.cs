using System;
using Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Commands;
using Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Context;

namespace Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Mappers;

internal static class CreateBranchRequestMapper
{
    internal static CreateBranchRequest ToCreateBranchRequest(this CreateBranchCommand createBranchCommand)
    {
        ArgumentNullException.ThrowIfNull(createBranchCommand);

        return new CreateBranchRequest
        {
            NewBranchName = createBranchCommand.NewBranchName,
            OldReferenceName = createBranchCommand.OldReferenceName
        };
    }
}