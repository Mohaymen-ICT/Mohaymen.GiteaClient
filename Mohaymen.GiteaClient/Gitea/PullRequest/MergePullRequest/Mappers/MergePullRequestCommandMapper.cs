using System;
using Mohaymen.GiteaClient.Gitea.PullRequest.MergePullRequest.Commands;
using Mohaymen.GiteaClient.Gitea.PullRequest.MergePullRequest.Dtos;

namespace Mohaymen.GiteaClient.Gitea.PullRequest.MergePullRequest.Mappers;

internal static class MergePullRequestCommandMapper
{
    internal static MergePullRequestCommand Map(this MergePullRequestCommandDto mergePullRequestCommandDto)
    {
        ArgumentNullException.ThrowIfNull(mergePullRequestCommandDto);

        return new MergePullRequestCommand
        {
            RepositoryName = mergePullRequestCommandDto.RepositoryName,
            Index = mergePullRequestCommandDto.Index,
            MergeType = mergePullRequestCommandDto.MergeType,
            DeleteBranchAfterMerge = mergePullRequestCommandDto.DeleteBranchAfterMerge,
            ForceMerge = mergePullRequestCommandDto.ForceMerge,
            MergeWhenChecksSucceed = mergePullRequestCommandDto.MergeWhenChecksSucceed,
            MergeTitle = mergePullRequestCommandDto.MergeTitle,
            MergeMessage = mergePullRequestCommandDto.MergeMessage,
        };
    }
}