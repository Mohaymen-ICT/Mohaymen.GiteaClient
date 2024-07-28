using FluentValidation;
using Mohaymen.GiteaClient.Core.Validation;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Commands;

namespace Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Validators;

internal class CreateCommitCommandValidator : AbstractValidator<CreateCommitCommand>
{
    public CreateCommitCommandValidator()
    {
        RuleFor(x => x.RepositoryName)
            .NotEmpty()
            .WithErrorCode(ValidationErrorCodes.EmptyRepositoryNameErrorCode)
            .WithMessage("repository name should not be empty");
        RuleFor(x => x.BranchName)
            .NotEmpty()
            .WithErrorCode(ValidationErrorCodes.EmptyBranchNameErrorCode)
            .WithMessage("branch name should not be empty");
        RuleFor(x => x.CommitMessage)
            .NotEmpty()
            .WithErrorCode(ValidationErrorCodes.EmptyCommitMessageErrorCode)
            .WithMessage("commit message should not be empty");
        RuleForEach(x => x.FileCommitCommands)
            .SetValidator(new FileCommitCommandModelValidator());
    }
}