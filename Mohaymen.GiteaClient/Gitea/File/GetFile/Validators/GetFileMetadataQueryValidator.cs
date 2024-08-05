using FluentValidation;
using Mohaymen.GiteaClient.Gitea.File.GetFile.Queries;

namespace Mohaymen.GiteaClient.Gitea.File.GetFile.Validators;

internal sealed class GetFileMetadataQueryValidator : AbstractValidator<GetFileMetadataQuery>
{
    public GetFileMetadataQueryValidator()
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