using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using Mohaymen.GiteaClient.Core.Configs;
using Mohaymen.GiteaClient.Gitea.Branch.Common.ApiCall.Abstractions;
using Mohaymen.GiteaClient.Gitea.Branch.Common.Dtos;
using Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Commands;
using Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Context;
using NSubstitute;
using Refit;
using Xunit;

namespace Mohaymen.GiteaClient.Tests.Gitea.Branch.CreateBranch.Commands;

public class CreateBranchCommandHandlerTests
{
    private readonly IBranchRestClient _branchRestClient;
    private readonly IOptions<GiteaApiConfiguration> _options;
    private readonly InlineValidator<CreateBranchCommand> _validator;
    private readonly IRequestHandler<CreateBranchCommand, ApiResponse<BranchResponseDto>> _sut;

    public CreateBranchCommandHandlerTests()
    {
        _branchRestClient = Substitute.For<IBranchRestClient>();
        _options = Substitute.For<IOptions<GiteaApiConfiguration>>();
        _validator = new InlineValidator<CreateBranchCommand>();
        _sut = new CreateBranchCommandHandler(_branchRestClient, _options, _validator);
    }

    [Fact]
    public async Task Handle_ShouldThrowsValidationException_WhenInputIsInvalid()
    {
        // Arrange
        _validator.RuleFor(x => x).Must(_ => false);
        var command = new CreateBranchCommand
        {
            RepositoryName = "repo",
            NewBranchName = "new_branch",
            OldReferenceName = "old_ref"
        };

        // Act
        var actual = async () => await _sut.Handle(command, default);

        // Assert
        await actual.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task Handle_ShouldCallCreateBranchAsync_AndInputsAreValid()
    {
        // Arrange
        _validator.RuleFor(x => x).Must(_ => true);
        const string owner = "owner";
        const string repositoryName = "repo";
        const string newBranchName = "new_branch";
        const string oldReferenceName = "old_ref";
        var command = new CreateBranchCommand
        {
            RepositoryName = repositoryName,
            NewBranchName = newBranchName,
            OldReferenceName = oldReferenceName
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
        await _branchRestClient.Received(1).CreateBranchAsync(owner,
            repositoryName,
            Arg.Is<CreateBranchRequest>(x => x.NewBranchName == newBranchName
                                             && x.OldReferenceName == oldReferenceName));
    }
}