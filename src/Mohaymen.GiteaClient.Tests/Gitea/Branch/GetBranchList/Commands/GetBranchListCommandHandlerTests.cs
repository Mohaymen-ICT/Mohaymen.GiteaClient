using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using Mohaymen.GiteaClient.Core.Configs;
using Mohaymen.GiteaClient.Gitea.Branch.Common.ApiCall.Abstractions;
using Mohaymen.GiteaClient.Gitea.Branch.Common.Dtos;
using Mohaymen.GiteaClient.Gitea.Branch.GetBranchList.Commands;
using NSubstitute;
using Refit;
using Xunit;

namespace Mohaymen.GiteaClient.Tests.Gitea.Branch.GetBranchList.Commands;

public class GetBranchListCommandHandlerTests
{
    private readonly IBranchRestClient _branchRestClient;
    private readonly IOptions<GiteaApiConfiguration> _options;
    private readonly InlineValidator<GetBranchListCommand> _validator;
    private readonly IRequestHandler<GetBranchListCommand, ApiResponse<List<BranchResponseDto>>> _sut;

    public GetBranchListCommandHandlerTests()
    {
        _branchRestClient = Substitute.For<IBranchRestClient>();
        _options = Substitute.For<IOptions<GiteaApiConfiguration>>();
        _validator = new InlineValidator<GetBranchListCommand>();
        _sut = new GetBranchListCommandHandler(_branchRestClient, _options, _validator);
    }

    [Fact]
    public async Task Handle_ShouldThrowsValidationException_WhenInputIsInvalid()
    {
        // Arrange
        _validator.RuleFor(x => x).Must(_ => false);
        var command = new GetBranchListCommand
        {
            RepositoryName = "repo"
        };

        // Act
        var actual = async () => await _sut.Handle(command, default);

        // Assert
        await actual.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task Handle_ShouldCallGetBranchListAsync_AndInputsAreValid()
    {
        // Arrange
        _validator.RuleFor(x => x).Must(_ => true);
        const string owner = "owner";
        const string repositoryName = "repo";
        var command = new GetBranchListCommand
        {
            RepositoryName = repositoryName
        };
        _options.Value.Returns(new GiteaApiConfiguration
        {
            BaseUrl = "url",
            PersonalAccessToken = "token",
            RepositoriesOwner = owner
        });

        // Act
        await _sut.Handle(command, default);
        // Assert
        await _branchRestClient.Received(1).GetBranchListAsync(owner, repositoryName);
    }
}