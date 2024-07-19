using FluentValidation;
using Mohaymen.GiteaClient.Core.Validation;
using Mohaymen.GiteaClient.Gitea.Repository.SearchRepository.Queries;

namespace Mohaymen.GiteaClient.Gitea.Repository.SearchRepository.Validators;

internal class SearchRepositoryQueryValidator : AbstractValidator<SearchRepositoryQuery>
{
    public SearchRepositoryQueryValidator()
    {
        RuleFor(x => x.Query)
            .NotEmpty()
            .WithErrorCode(ValidationErrorCodes.EmptySearchQueryErrorCode)
            .WithMessage("the search query is null or empty!");
    }
}