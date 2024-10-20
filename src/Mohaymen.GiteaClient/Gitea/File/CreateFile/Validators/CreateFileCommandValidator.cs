using FluentValidation;
using Mohaymen.GiteaClient.Gitea.File.CreateFile.Commands;

namespace Mohaymen.GiteaClient.Gitea.File.CreateFile.Validators;

public class CreateFileCommandValidator : AbstractValidator<CreateFileCommand>
{
    public CreateFileCommandValidator()
    {
        RuleFor(x => x.RepositoryName)
            .NotEmpty()
            .WithErrorCode(CreateFileErrorCodes.EmptyRepositoryNameErrorCode)
            .WithMessage("repository name should not be empty");
        RuleFor(x => x.FilePath)
            .NotEmpty()
            .WithErrorCode(CreateFileErrorCodes.EmptyFilePathErrorCode)
            .WithMessage("file path should not be empty");
        RuleFor(x => x.Content)
            .NotEmpty()
            .WithErrorCode(CreateFileErrorCodes.EmptyContentErrorCode)
            .WithMessage("content should not be empty");
    }
}