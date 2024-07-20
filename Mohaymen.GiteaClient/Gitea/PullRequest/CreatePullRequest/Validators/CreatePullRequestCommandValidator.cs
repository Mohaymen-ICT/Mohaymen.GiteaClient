using FluentValidation;
using Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Commands;
using Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Validators;

namespace Mohaymen.GiteaClient.Gitea.PullRequest.CreatePullRequest.Validators;

internal sealed class CreatePullRequestCommandValidator : AbstractValidator<CreateBranchCommand>
{
    public CreatePullRequestCommandValidator()
    {
        RuleFor(x => x.RepositoryName)
            .NotEmpty()
            .WithErrorCode(CreateBranchErrorCodes.EmptyRepositoryNameErrorCode)
            .WithMessage("repository name should not be empty");
    }
}
