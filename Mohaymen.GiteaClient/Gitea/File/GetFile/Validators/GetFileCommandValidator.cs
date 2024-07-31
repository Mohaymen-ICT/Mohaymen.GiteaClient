using FluentValidation;
using Mohaymen.GiteaClient.Gitea.File.GetFile.Commands;

namespace Mohaymen.GiteaClient.Gitea.File.GetFile.Validators;

internal sealed class GetFileCommandValidator : AbstractValidator<GetFileCommand>
{
    public GetFileCommandValidator()
    {
        RuleFor(x => x.RepositoryName)
            .NotEmpty()
            .WithErrorCode(GetFileErrorCodes.EmptyRepositoryNameErrorCode)
            .WithMessage("repository name should not be empty");
        RuleFor(x => x.FilePath)
            .NotEmpty()
            .WithErrorCode(GetFileErrorCodes.EmptyFilePathErrorCode)
            .WithMessage("file path should not be empty");
    }
}