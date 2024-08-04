using FluentValidation;
using Mohaymen.GiteaClient.Gitea.PullRequest.CreatePullRequest.Validators;
using Mohaymen.GiteaClient.Gitea.PullRequest.MergePullRequest.Commands;

namespace Mohaymen.GiteaClient.Gitea.PullRequest.MergePullRequest.Validators;

internal sealed class MergePullRequestCommandValidator : AbstractValidator<MergePullRequestCommand>
{
    public MergePullRequestCommandValidator()
    {
        RuleFor(x => x.RepositoryName)
            .NotEmpty()
            .WithErrorCode(MergePullRequestErrorCodes.EmptyRepositoryNameErrorCode)
            .WithMessage("repository name should not be empty");
        
        RuleFor(x => x.Index)
            .GreaterThan(0)
            .WithErrorCode(MergePullRequestErrorCodes.IndexLessThanOneErrorCode)
            .WithMessage("index should be greater than zero");
    }
}
