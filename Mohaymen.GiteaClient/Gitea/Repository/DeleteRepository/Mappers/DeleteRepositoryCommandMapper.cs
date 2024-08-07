using System;
using Mohaymen.GiteaClient.Gitea.Repository.DeleteRepository.Commands;
using Mohaymen.GiteaClient.Gitea.Repository.DeleteRepository.Dto;

namespace Mohaymen.GiteaClient.Gitea.Repository.DeleteRepository.Mappers;

internal static class DeleteRepositoryCommandMapper
{
    public static DeleteRepositoryCommand Map(this DeleteRepositoryCommandDto deleteRepositoryCommandDto)
    {
        if (deleteRepositoryCommandDto is null)
        {
            throw new ArgumentNullException(nameof(deleteRepositoryCommandDto));
        }
        
        return new DeleteRepositoryCommand
        {
            RepositoryName = deleteRepositoryCommandDto.RepositoryName
        };
    }
}