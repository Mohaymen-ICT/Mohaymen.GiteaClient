﻿using System;
using Mohaymen.GiteaClient.Gitea.PullRequest.GetPullRequestList.Commands;
using Mohaymen.GiteaClient.Gitea.PullRequest.GetPullRequestList.Dtos;

namespace Mohaymen.GiteaClient.Gitea.PullRequest.GetPullRequestList.Mappers;

internal static class GetPullRequestListCommandMapper
{
    internal static GetPullRequestListCommand Map(this GetPullRequestListCommandDto getPullRequestListCommandDto)
    {
        if (getPullRequestListCommandDto is null)
        {
            throw new ArgumentNullException(nameof(getPullRequestListCommandDto));
        }

        return new GetPullRequestListCommand
        {
            RepositoryName = getPullRequestListCommandDto.RepositoryName,
            State = getPullRequestListCommandDto.State,
            SortBy = getPullRequestListCommandDto.SortBy,
            LabelIds = getPullRequestListCommandDto.LabelIds
        };
    }
}