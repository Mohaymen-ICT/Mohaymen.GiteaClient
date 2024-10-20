using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Mohaymen.GiteaClient.Gitea.Branch.Common.Dtos;
using Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Dtos;
using Mohaymen.GiteaClient.Gitea.Branch.GetBranchList.Dtos;
using Refit;

namespace Mohaymen.GiteaClient.Gitea.Branch.Common.Facade.Abstractions;

public interface IBranchFacade
{
    Task<ApiResponse<BranchResponseDto>> CreateBranchAsync(CreateBranchCommandDto createBranchCommandDto, CancellationToken cancellationToken);
    Task<ApiResponse<List<BranchResponseDto>>> GetBranchListAsync(GetBranchListCommandDto getBranchListCommandDto, CancellationToken cancellationToken);
}