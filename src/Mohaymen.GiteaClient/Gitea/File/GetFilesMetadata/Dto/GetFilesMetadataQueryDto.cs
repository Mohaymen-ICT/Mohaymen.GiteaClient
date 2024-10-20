namespace Mohaymen.GiteaClient.Gitea.File.GetFilesMetadata.Dto;

public class GetFilesMetadataQueryDto
{
    public required string RepositoryName { get; init; }
    public required string BranchName { get; init; }
}