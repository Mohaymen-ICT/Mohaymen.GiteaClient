using FluentAssertions;
using FluentValidation;
using MediatR;
using Mohaymen.GiteaClient.Gitea.Repository.Common.ApiCall.Abstractions;
using Mohaymen.GiteaClient.Gitea.Repository.SearchRepository.Dtos;
using Mohaymen.GiteaClient.Gitea.Repository.SearchRepository.Queries;
using NSubstitute;
using Refit;
using Xunit;

namespace Mohaymen.GiteaClient.Tests.Gitea.Repository.SearchRepository.Queries;

public class SearchRepositoryQueryHandlerTests
{
    private readonly InlineValidator<SearchRepositoryQuery> _validator;
    private readonly IRepositoryRestClient _repositoryRestClient;
    private readonly IRequestHandler<SearchRepositoryQuery, ApiResponse<SearchRepositoryResponseDto>> _sut;

    public SearchRepositoryQueryHandlerTests()
    {
        _validator = new InlineValidator<SearchRepositoryQuery>();
        _repositoryRestClient = Substitute.For<IRepositoryRestClient>();
        _sut = new SearchRepositoryQueryHandler(_validator, _repositoryRestClient);
    }

    [Fact]
    public async Task Handle_ShouldThrowValidationException_WhenInputIsNotValid()
    {
        // Arrange
        var searchRepositoryQuery = new SearchRepositoryQuery
        {
            Query = ""
        };
        _validator.RuleFor(x => x).Must(x => false);
        
        // Act
        var actual = async () => await _sut.Handle(searchRepositoryQuery, default);

        // Assert
        await actual.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task Handle_ShouldCallSearchRepositoryAsync_WhenInputIsProvidedPropery()
    {
        // Arrange
        const string query = "fakeQuery";
        const int page = 1;
        const int limit = 5;
        var searchRepositoryQuery = new SearchRepositoryQuery
        {
            Query = query,
            Page = page,
            Limit = limit
        };
        
        // Act
        await _sut.Handle(searchRepositoryQuery, default);

        // Assert
        await _repositoryRestClient.Received(1).SearchRepositoryAsync(query, page, limit);
    }
}