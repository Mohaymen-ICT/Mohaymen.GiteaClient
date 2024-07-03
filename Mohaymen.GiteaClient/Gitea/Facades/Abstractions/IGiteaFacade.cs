using System.Threading;
using System.Threading.Tasks;
using Mohaymen.GitClient.APICall.Domain;
using Mohaymen.GitClient.Gitea.Business.Commands.Repository.CreateRepository;

namespace Mohaymen.GitClient.Gitea.Facades.Abstractions;

public interface IGiteaFacade
{
    Task<GiteaResponseDto<CreateRepositoryResponseDto>> CreateRepository(CreateRepositoryCommand createRepositoryCommand, CancellationToken cancellationToken);
}