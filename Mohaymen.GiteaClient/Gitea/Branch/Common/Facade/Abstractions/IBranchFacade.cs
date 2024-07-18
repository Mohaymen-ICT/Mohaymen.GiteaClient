using System.Threading;
using System.Threading.Tasks;
using Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Dtos;
using Refit;

namespace Mohaymen.GiteaClient.Gitea.Branch.Common.Facade.Abstractions;

public interface IBranchFacade
{
    Task<ApiResponse<CreateBranchResponseDto>> CreateBranchAsync(CreateBranchCommandDto createBranchCommandDto, CancellationToken cancellationToken);
}