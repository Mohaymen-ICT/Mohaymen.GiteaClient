using FluentValidation;
using Mohaymen.GiteaClient.Core.Validation;
using Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Commands;

namespace Mohaymen.GiteaClient.Gitea.Repository.CreateRepository.Validators;

internal sealed class CreateRepositoryCommandValidator : AbstractValidator<CreateRepositoryCommand>
{
    public CreateRepositoryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithErrorCode(ValidationErrorCodes.EmptyRepositoryNameErrorCode)
            .WithMessage("repository name should not be empty");
        RuleFor(x => x.DefaultBranch)
            .NotEmpty()
            .WithErrorCode(ValidationErrorCodes.EmptyBranchNameErrorCode)
            .WithMessage("branch name should not be empty");
    }
}