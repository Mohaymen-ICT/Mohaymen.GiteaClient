using Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Commands;
using Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Context;
using Riok.Mapperly.Abstractions;

namespace Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Mappers;

[Mapper]
internal static partial class CreateRepositoryRequestMapper
{
    public static partial CreateRepositoryRequest Map(CreateRepositoryCommand createRepositoryCommandDto);
}