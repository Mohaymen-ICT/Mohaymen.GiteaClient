using System;
using Mohaymen.GiteaClient.Gitea.Repository.SearchRepository.Context;
using Mohaymen.GiteaClient.Gitea.Repository.SearchRepository.Queries;

namespace Mohaymen.GiteaClient.Gitea.Repository.SearchRepository.Mappers;

internal static class SearchRepositoryRequestMapper
{
    public static SearchRepositoryRequest Map(this SearchRepositoryQuery searchRepositoryQuery)
    {
        ArgumentNullException.ThrowIfNull(searchRepositoryQuery);

        return new SearchRepositoryRequest
        {
            Query = searchRepositoryQuery.Query
        };
    }
}