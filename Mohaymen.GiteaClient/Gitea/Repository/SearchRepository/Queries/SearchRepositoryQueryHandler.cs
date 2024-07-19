using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Mohaymen.GiteaClient.Gitea.Repository.Common.ApiCall.Abstractions;
using Mohaymen.GiteaClient.Gitea.Repository.SearchRepository.Dtos;
using Mohaymen.GiteaClient.Gitea.Repository.SearchRepository.Mappers;
using Refit;

namespace Mohaymen.GiteaClient.Gitea.Repository.SearchRepository.Queries;

internal class SearchRepositoryQuery : IRequest<ApiResponse<SearchRepositoryResponseDto>>
{
    public required string Query { get; init; }
}

internal class SearchRepositoryQueryHandler : IRequestHandler<SearchRepositoryQuery, ApiResponse<SearchRepositoryResponseDto>>
{
    private readonly IValidator<SearchRepositoryQuery> _validator;
    private readonly IRepositoryRestClient _repositoryRestClient;

    public SearchRepositoryQueryHandler(IValidator<SearchRepositoryQuery> validator,
        IRepositoryRestClient repositoryRestClient)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _repositoryRestClient = repositoryRestClient ?? throw new ArgumentNullException(nameof(repositoryRestClient));
    }

    public async Task<ApiResponse<SearchRepositoryResponseDto>> Handle(SearchRepositoryQuery searchRepositoryQuery, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(searchRepositoryQuery);
        var request = searchRepositoryQuery.Map();
        return await _repositoryRestClient.SearchRepositoryAsync(request);
    }
}