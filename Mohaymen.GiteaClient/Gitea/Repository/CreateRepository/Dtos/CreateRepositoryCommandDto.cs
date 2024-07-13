using MediatR;
using Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Commands;
using Refit;

namespace Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Dtos;

public class CreateRepositoryCommandDto
{
    public string DefaultBranch { get; init; }

    public string Name { get; init; }

    public bool IsPrivateBranch { get; init; }
}