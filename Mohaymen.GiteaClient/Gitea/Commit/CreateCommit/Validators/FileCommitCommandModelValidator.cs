using FluentValidation;
using Mohaymen.GiteaClient.Core.Validation;
using Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Commands;

namespace Mohaymen.GiteaClient.Gitea.Commit.CreateCommit.Validators;

internal class FileCommitCommandModelValidator : AbstractValidator<FileCommitCommandModel>
{
    public FileCommitCommandModelValidator()
    {
        RuleFor(x => x.Path)
            .NotEmpty()
            .WithErrorCode(ValidationErrorCodes.InvalidFilePathErrorCode)
            .WithMessage("file path is empty");

        RuleFor(x => x.Content)
            .NotEmpty()
            .WithErrorCode(ValidationErrorCodes.InvalidFileContentErrorCode)
            .WithMessage("file content is empty");
        
    }
}