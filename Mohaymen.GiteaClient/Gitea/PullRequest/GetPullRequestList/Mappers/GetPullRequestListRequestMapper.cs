using System;
using Mohaymen.GiteaClient.Gitea.PullRequest.Common.Enums;
using Mohaymen.GiteaClient.Gitea.PullRequest.GetPullRequestList.Commands;
using Mohaymen.GiteaClient.Gitea.PullRequest.GetPullRequestList.Context;

namespace Mohaymen.GiteaClient.Gitea.PullRequest.GetPullRequestList.Mappers;

internal static class GetPullRequestListRequestMapper
{
    internal static GetPullRequestListRequest ToGetPullRequestListRequest(this GetPullRequestListCommand getPullRequestListCommand)
    {
        ArgumentNullException.ThrowIfNull(getPullRequestListCommand);

        return new GetPullRequestListRequest
        {
            State = getPullRequestListCommand.State.ToLowerString(),
            SortBy = getPullRequestListCommand.SortBy.ToLowerString(),
            LabelIds = getPullRequestListCommand.LabelIds
        };
    }
}