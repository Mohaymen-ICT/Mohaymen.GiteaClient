using System;
using Mohaymen.GiteaClient.Gitea.Commit.GetBranchCommits.Dtos;
using Mohaymen.GiteaClient.Gitea.Commit.GetBranchCommits.Queries;

namespace Mohaymen.GiteaClient.Gitea.Commit.GetBranchCommits.Mappers;

internal static class GetSingleCommitQueryMapper
{
    public static GetSingleCommitQuery Map(this GetSingleCommitQueryDto getSingleCommitQueryDto)
    {
        if (getSingleCommitQueryDto is null)
        {
            throw new ArgumentNullException(nameof(getSingleCommitQueryDto));
        }

        return new GetSingleCommitQuery
        {
            RepositoryName = getSingleCommitQueryDto.RepositoryName,
            CommitSha = getSingleCommitQueryDto.CommitSha
        };
    }
}