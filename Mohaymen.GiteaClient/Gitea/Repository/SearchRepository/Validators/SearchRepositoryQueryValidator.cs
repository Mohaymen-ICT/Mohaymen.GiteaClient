using FluentValidation;
using Mohaymen.GiteaClient.Core.Validation;
using Mohaymen.GiteaClient.Gitea.Repository.SearchRepository.Queries;

namespace Mohaymen.GiteaClient.Gitea.Repository.SearchRepository.Validators;

internal sealed class SearchRepositoryQueryValidator : AbstractValidator<SearchRepositoryQuery>
{
    public SearchRepositoryQueryValidator()
    {
        RuleFor(x => x.Query)
            .NotEmpty()
            .WithErrorCode(ValidationErrorCodes.EmptySearchQueryErrorCode)
            .WithMessage("the search query is null or empty!");
        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1)
            .WithErrorCode(ValidationErrorCodes.InvalidPageSizeErrorCode)
            .WithMessage("the page size must be greater than or equal to 1");
        RuleFor(x => x.Limit)
            .GreaterThan(0)
            .WithErrorCode(ValidationErrorCodes.InvalidLimitErrorCode)
            .WithMessage("the limit must be greater than 0");
    }
}