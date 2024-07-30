using FluentValidation;
using Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Validators;
using Mohaymen.GiteaClient.Gitea.PullRequest.CreatePullRequest.Commands;

namespace Mohaymen.GiteaClient.Gitea.PullRequest.CreatePullRequest.Validators;

internal sealed class CreatePullRequestCommandValidator : AbstractValidator<CreatePullRequestCommand>
{
    public CreatePullRequestCommandValidator()
    {
        RuleFor(x => x.RepositoryName)
            .NotEmpty()
            .WithErrorCode(CreatePullRequestErrorCodes.EmptyRepositoryNameErrorCode)
            .WithMessage("repository name should not be empty");
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithErrorCode(CreatePullRequestErrorCodes.EmptyTitleErrorCode)
            .WithMessage("title should not be empty");
        RuleFor(x => x.HeadBranch)
            .NotEmpty()
            .WithErrorCode(CreatePullRequestErrorCodes.EmptyHeadBranchErrorCode)
            .WithMessage("head branch should not be empty");
        RuleFor(x => x.BaseBranch)
            .NotEmpty()
            .WithErrorCode(CreatePullRequestErrorCodes.EmptyBaseBranchErrorCode)
            .WithMessage("base branch should not be empty");
    }
}
