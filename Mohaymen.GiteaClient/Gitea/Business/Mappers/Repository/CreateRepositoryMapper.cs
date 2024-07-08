using Mohaymen.GiteaClient.Gitea.Business.Commands.Repository.CreateRepository;
using Mohaymen.GiteaClient.Gitea.Domain.Dtos.Repository.CreateRepository;
using Riok.Mapperly.Abstractions;

namespace Mohaymen.GiteaClient.Gitea.Business.Mappers.Repository;

[Mapper]
internal static partial class CreateRepositoryMapper
{
    public static partial CreateRepositoryCommand Map(CreateRepositoryCommandDto createRepositoryCommandDto);
}