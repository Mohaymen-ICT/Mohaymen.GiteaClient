using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Dtos.Response;
using Refit;

namespace Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Commands;

internal class CreateCommitCommand : IRequest<ApiResponse<CreateCommitResponseDto>>
{
    public required string RepositoryName { get; init; }
    
    public required string BranchName { get; init; }
    
    public required string CommitMessage { get; init; }
    
    public required List<FileCommitCommandModel> FileCommitCommands { get; init; }
}

internal class CreateCommitCommandHandler : IRequestHandler<CreateCommitCommand, ApiResponse<CreateCommitResponseDto>>
{
    public Task<ApiResponse<CreateCommitResponseDto>> Handle(CreateCommitCommand request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}