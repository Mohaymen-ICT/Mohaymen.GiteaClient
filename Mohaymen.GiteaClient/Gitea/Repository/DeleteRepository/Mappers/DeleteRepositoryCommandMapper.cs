using System;
using Mohaymen.GiteaClient.Gitea.Repository.DeleteRepository.Commands;
using Mohaymen.GiteaClient.Gitea.Repository.DeleteRepository.Dto;

namespace Mohaymen.GiteaClient.Gitea.Repository.DeleteRepository.Mappers;

internal static class DeleteRepositoryCommandMapper
{
    public static DeleteRepositoryCommand Map(this DeleteRepositoryCommandDto deleteRepositoryCommandDto)
    {
        ArgumentNullException.ThrowIfNull(deleteRepositoryCommandDto);

        return new DeleteRepositoryCommand
        {
            RepositoryName = deleteRepositoryCommandDto.RepositoryName
        };
    }
}