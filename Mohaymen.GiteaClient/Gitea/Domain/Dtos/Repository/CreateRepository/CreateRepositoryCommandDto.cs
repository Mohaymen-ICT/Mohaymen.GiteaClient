namespace Mohaymen.GiteaClient.Gitea.Domain.Dtos.Repository.CreateRepository;

public sealed class CreateRepositoryCommandDto
{
    public string DefaultBranch { get; init; }

    public string Name { get; init; }

    public bool IsPrivateBranch { get; init; }
}