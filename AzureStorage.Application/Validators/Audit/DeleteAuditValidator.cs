namespace AzureStorage.Application.Validators.Audit
{
    using AzureStorage.Application.Features.Audit.Commands;
    using FluentValidation;

    public class DeleteAuditValidator : AbstractValidator<DeleteAuditCommand>
    {
        public DeleteAuditValidator()
        {
            RuleFor(r => r.Entity)
                .NotEmpty()
                .WithErrorCode("400")
                .WithMessage("The 'Entity' is required.");

            RuleFor(r => r.AuditId)
                .NotEmpty()
                .WithErrorCode("400")
                .WithMessage("The 'AuditId' is required.");
        }
    }
}
