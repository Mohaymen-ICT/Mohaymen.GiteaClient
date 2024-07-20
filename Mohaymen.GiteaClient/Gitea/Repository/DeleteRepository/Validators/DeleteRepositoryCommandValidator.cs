using FluentValidation;
using Mohaymen.GiteaClient.Core.Validation;
using Mohaymen.GiteaClient.Gitea.Repository.DeleteRepository.Commands;

namespace Mohaymen.GiteaClient.Gitea.Repository.DeleteRepository.Validators;

internal class DeleteRepositoryCommandValidator : AbstractValidator<DeleteRepositoryCommand>
{
    public DeleteRepositoryCommandValidator()
    {
        RuleFor(x => x.RepositoryName)
            .NotEmpty()
            .WithErrorCode(ValidationErrorCodes.EmptyRepositoryNameErrorCode)
            .WithMessage("repository name is empty.");
    }
}