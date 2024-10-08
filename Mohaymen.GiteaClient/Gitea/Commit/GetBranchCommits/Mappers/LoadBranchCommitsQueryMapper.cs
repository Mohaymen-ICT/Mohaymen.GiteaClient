﻿using System;
using Mohaymen.GiteaClient.Gitea.Commit.GetBranchCommits.Dtos;
using Mohaymen.GiteaClient.Gitea.Commit.GetBranchCommits.Queries;

namespace Mohaymen.GiteaClient.Gitea.Commit.GetBranchCommits.Mappers;

internal static class LoadBranchCommitsQueryMapper
{
    public static LoadBranchCommitsQuery Map(this LoadBranchCommitsQueryDto loadBranchCommitsQueryDto)
    {
        if (loadBranchCommitsQueryDto is null)
        {
            throw new ArgumentNullException(nameof(loadBranchCommitsQueryDto));
        }

        return new LoadBranchCommitsQuery
        {
            RepositoryName = loadBranchCommitsQueryDto.RepositoryName,
            BranchName = loadBranchCommitsQueryDto.BranchName,
            Limit = loadBranchCommitsQueryDto.Limit,
            Page = loadBranchCommitsQueryDto.Page
        };
    }
}