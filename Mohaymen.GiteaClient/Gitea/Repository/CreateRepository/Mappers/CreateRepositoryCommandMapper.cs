using System;
using Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Commands;
using Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Dtos;

namespace Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Mappers;

internal static class CreateRepositoryCommandMapper
{
    public static CreateRepositoryCommand Map(this CreateRepositoryCommandDto createRepositoryCommandDto)
    {
        ArgumentNullException.ThrowIfNull(createRepositoryCommandDto);

        return new CreateRepositoryCommand
        {
            DefaultBranch = createRepositoryCommandDto.DefaultBranch,
            Name = createRepositoryCommandDto.Name,
            IsPrivateBranch = createRepositoryCommandDto.IsPrivateBranch
        };
    }
}