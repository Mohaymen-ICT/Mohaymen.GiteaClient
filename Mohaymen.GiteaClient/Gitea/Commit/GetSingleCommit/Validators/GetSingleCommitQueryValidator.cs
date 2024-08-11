using FluentValidation;
using Mohaymen.GiteaClient.Core.Validation;
using Mohaymen.GiteaClient.Gitea.Commit.GetBranchCommits.Queries;

namespace Mohaymen.GiteaClient.Gitea.Commit.GetBranchCommits.Validators;

internal sealed class GetSingleCommitQueryValidator : AbstractValidator<GetSingleCommitQuery>
{
    public GetSingleCommitQueryValidator()
    {
        RuleFor(x => x.RepositoryName)
            .NotEmpty()
            .WithErrorCode(ValidationErrorCodes.EmptyRepositoryNameErrorCode)
            .WithMessage("repository name should not be empty");
        RuleFor(x => x.CommitSha)
            .NotEmpty()
            .WithErrorCode(ValidationErrorCodes.EmptyCommitShaErrorCode)
            .WithMessage("commit hash(sha) should not be empty");
    }
}