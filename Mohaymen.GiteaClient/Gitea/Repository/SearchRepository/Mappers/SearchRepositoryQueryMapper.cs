using System;
using Mohaymen.GiteaClient.Gitea.Repository.SearchRepository.Dtos;
using Mohaymen.GiteaClient.Gitea.Repository.SearchRepository.Queries;

namespace Mohaymen.GiteaClient.Gitea.Repository.SearchRepository.Mappers;

internal static class SearchRepositoryQueryMapper
{
    public static SearchRepositoryQuery Map(this SearchRepositoryQueryDto searchRepositoryQueryDto)
    {
        if (searchRepositoryQueryDto is null)
        {
            throw new ArgumentNullException(nameof(searchRepositoryQueryDto));
        }

        return new SearchRepositoryQuery
        {
            Query = searchRepositoryQueryDto.Query,
            Limit = searchRepositoryQueryDto.Limit,
            Page = searchRepositoryQueryDto.Page
        };
    }
}