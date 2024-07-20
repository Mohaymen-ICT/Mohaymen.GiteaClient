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
            .WithErrorCode(CreateBranchErrorCodes.EmptyRepositoryNameErrorCode)
            .WithMessage("repository name should not be empty");
    }
}
