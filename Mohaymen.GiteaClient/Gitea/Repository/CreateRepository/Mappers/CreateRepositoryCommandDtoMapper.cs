using Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Commands;
using Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Dtos;
using Riok.Mapperly.Abstractions;

namespace Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Mappers;

[Mapper]
internal static partial class CreateRepositoryCommandDtoMapper
{
    public static partial CreateRepositoryCommand Map(CreateRepositoryCommandDto createRepositoryCommandDto);
}