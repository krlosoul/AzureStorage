namespace AzureStorage.Application.Validators.Audit
{
    using AzureStorage.Application.Features.Audit.Commands;
    using FluentValidation;

    public class UpdateAuditValidator : AbstractValidator<UpdateAuditCommand>
    {
        public UpdateAuditValidator()
        {
            RuleFor(r => r.Entity)
                .NotEmpty()
                .WithErrorCode("400")
                .WithMessage("The 'Entity' is required.");

            RuleFor(r => r.AuditId)
                .NotEmpty()
                .WithErrorCode("400")
                .WithMessage("The 'AuditId' is required.");

            RuleFor(r => r.TransactionTypeId)
                .InclusiveBetween(1, 3)
                .WithErrorCode("400")
                .WithMessage("The 'TransactionTypeId' must be between 1 and 3.");

            RuleFor(r => r.User)
                .NotEmpty()
                .WithErrorCode("400")
                .WithMessage("The 'User' is required.");

            RuleFor(r => r.NewData)
                .NotEmpty()
                .WithErrorCode("400")
                .WithMessage("The 'NewData' is required.");

            RuleFor(r => r.OldData)
                .NotEmpty()
                .WithErrorCode("400")
                .WithMessage("The 'OldData' is required.");
        }
    }
}
