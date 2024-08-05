using FluentValidation;
using Mohaymen.GiteaClient.Core.Validation;
using Mohaymen.GiteaClient.Gitea.File.GetFilesMetadata.Queries;

namespace Mohaymen.GiteaClient.Gitea.File.GetFilesMetadata.Validators;

internal class GetFilesMetadataQueryValidator : AbstractValidator<GetFilesMetadataQuery>
{
    public GetFilesMetadataQueryValidator()
    {
        RuleFor(x => x.RepositoryName)
            .NotEmpty()
            .WithErrorCode(ValidationErrorCodes.EmptyRepositoryNameErrorCode);

        RuleFor(x => x.BranchName)
            .NotEmpty()
            .WithErrorCode(ValidationErrorCodes.EmptyBranchNameErrorCode);
    }
}