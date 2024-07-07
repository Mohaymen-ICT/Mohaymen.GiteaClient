﻿using FluentValidation;
using Mohaymen.GitClient.Common.Validation;

namespace Mohaymen.GitClient.Gitea.Business.Commands.Repository.CreateRepository;

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