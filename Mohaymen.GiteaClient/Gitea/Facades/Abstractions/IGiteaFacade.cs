using System.Threading;
using System.Threading.Tasks;
using Mohaymen.GiteaClient.APICall.Domain;
using Mohaymen.GiteaClient.Gitea.Domain.Dtos.Repository.CreateRepository;

namespace Mohaymen.GiteaClient.Gitea.Facades.Abstractions;

public interface IGiteaFacade
{
    Task<GiteaResponseDto<CreateRepositoryResponseDto>> CreateRepository(CreateRepositoryCommandDto createRepositoryCommandDto, CancellationToken cancellationToken);
}