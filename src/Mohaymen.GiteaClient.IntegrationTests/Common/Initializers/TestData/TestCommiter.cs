using Mohaymen.GiteaClient.Gitea.Commit.Common.Facades.Abstractions;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Dtos.Request;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Dtos.Response;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData.Abstractions;
using Refit;

namespace Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData;

internal class TestCommiter : ITestCommiter
{
    private readonly ICommitFacade _commitFacade;

    public TestCommiter(ICommitFacade commitFacade)
    {
        _commitFacade = commitFacade ?? throw new ArgumentNullException(nameof(commitFacade));
    }

    public async Task<ApiResponse<CreateCommitResponseDto>?> CreateFileAsync(string repositoryName,
        string branchName,
        string filePath,
        string commitMessage,
        CancellationToken cancellationToken)
    {
        var createCommitDto = new CreateCommitCommandDto
        {
            RepositoryName = repositoryName,
            BranchName = branchName,
            CommitMessage = commitMessage,
            FileDtos =
            [
                new FileCommitDto
                {
                    Path = filePath,
                    Content = Convert.ToBase64String("sample test content"u8.ToArray()),
                    CommitActionDto = CommitActionDto.Create
                }
            ]
        };

        return await _commitFacade.CreateCommitAsync(createCommitDto, cancellationToken);
    }
}