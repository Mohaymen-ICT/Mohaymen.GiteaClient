using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using Mohaymen.GiteaClient.Core.Configs;
using Mohaymen.GiteaClient.Gitea.PullRequest.Common.ApiCall.Abstractions;
using Mohaymen.GiteaClient.Gitea.PullRequest.Common.Enums;
using Mohaymen.GiteaClient.Gitea.PullRequest.Common.Enums.Extensions;
using Mohaymen.GiteaClient.Gitea.PullRequest.CreatePullRequest.Commands;
using Mohaymen.GiteaClient.Gitea.PullRequest.CreatePullRequest.Context;
using Mohaymen.GiteaClient.Gitea.PullRequest.MergePullRequest.Commands;
using Mohaymen.GiteaClient.Gitea.PullRequest.MergePullRequest.Context;
using NSubstitute;
using Refit;
using Xunit;

namespace Mohaymen.GiteaClient.Tests.Gitea.PullRequest.MergePullRequest.Commands;

public class MergePullRequestCommandHandlerTests
{
    private readonly IPullRequestRestClient _pullRequestRestClient;
    private readonly IOptions<GiteaApiConfiguration> _options;
    private readonly InlineValidator<MergePullRequestCommand> _validator;
    private readonly IRequestHandler<MergePullRequestCommand, ApiResponse<Unit>> _sut;

    public MergePullRequestCommandHandlerTests()
    {
        _pullRequestRestClient = Substitute.For<IPullRequestRestClient>();
        _options = Substitute.For<IOptions<GiteaApiConfiguration>>();
        _validator = new InlineValidator<MergePullRequestCommand>();
        _sut = new MergePullRequestCommandHandler(_pullRequestRestClient, _options, _validator);
    }

    [Fact]
    public async Task Handle_ShouldThrowsValidationException_WhenInputIsInvalid()
    {
        // Arrange
        _validator.RuleFor(x => x).Must(_ => false);
        var command = new MergePullRequestCommand
        {
            RepositoryName = "repo",
            Index = 1,
            MergeType = MergeType.Merge
        };

        // Act
        var actual = async () => await _sut.Handle(command, default);

        // Assert
        await actual.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task Handle_ShouldCallMergePullRequestAsync_AndInputsAreValid()
    {
        // Arrange
        _validator.RuleFor(x => x).Must(_ => true);
        var owner = "owner";
        var repositoryName = "repo";
        var index = 1;
        var mergeType = MergeType.Merge;
        var command = new MergePullRequestCommand
        {
            RepositoryName = repositoryName,
            Index = index,
            MergeType = mergeType
        };
        _options.Value.Returns(new GiteaApiConfiguration
        {
            BaseUrl = "url",
            PersonalAccessToken = "token",
            RepositoriesOwner = owner
        });
        var expected = new MergePullRequestRequest
        {
            MergeType = mergeType.Normalize()
        };

        // Act
        await _sut.Handle(command, default);
        
        // Assert
        await _pullRequestRestClient.Received(1).MergePullRequestAsync(owner,
            repositoryName,
            index,
            Arg.Is<MergePullRequestRequest>(x => x.Equals(expected)),
            default);
    }
}