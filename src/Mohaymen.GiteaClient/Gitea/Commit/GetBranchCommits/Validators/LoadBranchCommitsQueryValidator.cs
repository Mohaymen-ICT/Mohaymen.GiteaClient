using FluentValidation;
using Mohaymen.GiteaClient.Core.Validation;
using Mohaymen.GiteaClient.Gitea.Commit.GetBranchCommits.Queries;

namespace Mohaymen.GiteaClient.Gitea.Commit.GetBranchCommits.Validators;

internal sealed class LoadBranchCommitsQueryValidator : AbstractValidator<LoadBranchCommitsQuery>
{
    public LoadBranchCommitsQueryValidator()
    {
        RuleFor(x => x.RepositoryName)
            .NotEmpty()
            .WithErrorCode(ValidationErrorCodes.EmptyRepositoryNameErrorCode)
            .WithMessage("repository name should not be empty");
        
        RuleFor(x => x.BranchName)
            .NotEmpty()
            .WithErrorCode(ValidationErrorCodes.EmptyBranchNameErrorCode)
            .WithMessage("branch name should not be empty");

        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1)
            .WithErrorCode(ValidationErrorCodes.InvalidPageSizeErrorCode)
            .WithMessage("page size should be greater than 0");

        RuleFor(x => x.Limit)
            .GreaterThanOrEqualTo(1)
            .WithErrorCode(ValidationErrorCodes.InvalidLimitErrorCode)
            .WithMessage("limit should be greater than 0");
    }
}