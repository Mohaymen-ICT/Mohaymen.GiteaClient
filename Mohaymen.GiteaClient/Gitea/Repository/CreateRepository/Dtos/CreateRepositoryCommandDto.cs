using MediatR;
using Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Commands;
using Refit;

namespace Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Dtos;

public class CreateRepositoryCommandDto
{
    public required string DefaultBranch { get; init; }

    public required string Name { get; init; }

    public bool IsPrivateBranch { get; init; }
}