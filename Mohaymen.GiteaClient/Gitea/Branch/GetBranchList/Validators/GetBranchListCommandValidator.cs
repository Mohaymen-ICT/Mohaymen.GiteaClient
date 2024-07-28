using FluentValidation;
using Mohaymen.GiteaClient.Gitea.Branch.GetBranchList.Commands;

namespace Mohaymen.GiteaClient.Gitea.Branch.GetBranchList.Validators;

internal sealed class GetBranchListCommandValidator : AbstractValidator<GetBranchListCommand>
{
    public GetBranchListCommandValidator()
    {
        RuleFor(x => x.RepositoryName)
            .NotEmpty()
            .WithErrorCode(GetBranchListErrorCodes.EmptyRepositoryNameErrorCode)
            .WithMessage("repository name should not be empty");
    }
}