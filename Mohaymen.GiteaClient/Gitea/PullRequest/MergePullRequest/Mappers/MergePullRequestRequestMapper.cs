using System;
using Mohaymen.GiteaClient.Gitea.PullRequest.Common.Enums.Extensions;
using Mohaymen.GiteaClient.Gitea.PullRequest.MergePullRequest.Commands;
using Mohaymen.GiteaClient.Gitea.PullRequest.MergePullRequest.Context;

namespace Mohaymen.GiteaClient.Gitea.PullRequest.MergePullRequest.Mappers;

internal static class MergePullRequestRequestMapper
{
    internal static MergePullRequestRequest Map(this MergePullRequestCommand mergePullRequestCommand)
    {
        if (mergePullRequestCommand is null)
        {
            throw new ArgumentNullException(nameof(mergePullRequestCommand));
        }

        return new MergePullRequestRequest
        {
            MergeType = mergePullRequestCommand.MergeType.Normalize(),
            DeleteBranchAfterMerge = mergePullRequestCommand.DeleteBranchAfterMerge,
            ForceMerge = mergePullRequestCommand.ForceMerge,
            MergeWhenChecksSucceed = mergePullRequestCommand.MergeWhenChecksSucceed,
            MergeTitle = mergePullRequestCommand.MergeTitle,
            MergeMessage = mergePullRequestCommand.MergeMessage
        };
    }
}