namespace Mohaymen.GiteaClient.Gitea.Repository.SearchRepository.Dtos;

public sealed class SearchRepositoryQueryDto
{
    public required string Query { get; init; }
    public int Page { get; init; }
    public int Limit { get; init; }
}