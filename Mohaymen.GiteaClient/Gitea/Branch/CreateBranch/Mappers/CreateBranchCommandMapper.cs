using System;
using Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Commands;
using Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Dtos;

namespace Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Mappers;

internal static class CreateBranchCommandMapper
{
    internal static CreateBranchCommand ToCreateBranchCommand(this CreateBranchCommandDto createBranchCommandDto)
    {
        ArgumentNullException.ThrowIfNull(createBranchCommandDto);

        return new CreateBranchCommand
        {
            RepositoryName = createBranchCommandDto.RepositoryName,
            NewBranchName = createBranchCommandDto.NewBranchName,
            OldReferenceName = createBranchCommandDto.OldReferenceName
        };
    }
}