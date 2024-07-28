using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using Mohaymen.GiteaClient.Core.Configs;
using Mohaymen.GiteaClient.Gitea.Commit.Common.Facades.Abstractions;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Dtos.Request;
using Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Dtos;
using Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData.Abstractions;
using Mohaymen.GiteaClient.IntegrationTests.Common.Models;
using Mohaymen.GiteaClient.IntegrationTests.Common.Models.Requests;
using Newtonsoft.Json;

namespace Mohaymen.GiteaClient.IntegrationTests.Common.Initializers.TestData;

internal class TestCommiter : ITestCommiter
{
    private readonly ICommitFacade _commitFacade;
    private readonly IOptions<GiteaApiConfiguration> _giteaOptions;

    public TestCommiter(IOptions<GiteaApiConfiguration> giteaOptions, ICommitFacade commitFacade)
    {
        _commitFacade = commitFacade ?? throw new ArgumentNullException(nameof(commitFacade));
        _giteaOptions = giteaOptions ?? throw new ArgumentNullException(nameof(giteaOptions));
    }

    public async Task CreateFileAsync(string repositoryName,
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

        await _commitFacade.CreateCommitAsync(createCommitDto, cancellationToken);
    }
}