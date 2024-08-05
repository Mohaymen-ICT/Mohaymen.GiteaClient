namespace Mohaymen.GiteaClient.Gitea.File.GetRepositoryFile.Dtos;

public class GetFileMetadataQueryDto
{
    public required string RepositoryName { get; init; }
    public required string FilePath { get; init; }
    public string? ReferenceName { get; init; }
}