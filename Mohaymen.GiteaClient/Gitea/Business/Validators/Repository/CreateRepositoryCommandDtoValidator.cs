using FluentValidation;
using Mohaymen.GiteaClient.Common.Validation;
using Mohaymen.GiteaClient.Gitea.Domain.Dtos.Repository.CreateRepository;

namespace Mohaymen.GiteaClient.Gitea.Business.Validators.Repository;

internal sealed class CreateRepositoryCommandDtoValidator : AbstractValidator<CreateRepositoryCommandDto>
{
    public CreateRepositoryCommandDtoValidator()
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