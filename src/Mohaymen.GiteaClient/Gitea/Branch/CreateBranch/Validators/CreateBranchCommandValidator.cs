using FluentValidation;
using Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Commands;

namespace Mohaymen.GiteaClient.Gitea.Branch.CreateBranch.Validators;

internal sealed class CreateBranchCommandValidator : AbstractValidator<CreateBranchCommand>
{
    public CreateBranchCommandValidator()
    {
        RuleFor(x => x.RepositoryName)
            .NotEmpty()
            .WithErrorCode(CreateBranchErrorCodes.EmptyRepositoryNameErrorCode)
            .WithMessage("repository name should not be empty");
        RuleFor(x => x.NewBranchName)
            .NotEmpty()
            .WithErrorCode(CreateBranchErrorCodes.EmptyNewBranchNameErrorCode)
            .WithMessage("new branch name should not be empty");
        RuleFor(x => x.OldReferenceName)
            .NotEmpty()
            .WithErrorCode(CreateBranchErrorCodes.EmptyOldReferenceNameErrorCode)
            .WithMessage("old reference name should not be empty");
        
    }
}
