using System;
using Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Commands;
using Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Context;

namespace Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Mappers;

internal static class CreateRepositoryRequestMapper
{
    public static CreateRepositoryRequest Map(CreateRepositoryCommand createRepositoryCommand)
    {
        ArgumentNullException.ThrowIfNull(createRepositoryCommand);

        return new CreateRepositoryRequest
        {
            DefaultBranch = createRepositoryCommand.DefaultBranch,
            Name = createRepositoryCommand.Name,
            IsPrivateBranch = createRepositoryCommand.IsPrivateBranch
        };
    }
}