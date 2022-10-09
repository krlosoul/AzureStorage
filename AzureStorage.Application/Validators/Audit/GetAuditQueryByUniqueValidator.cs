namespace AzureStorage.Application.Validators.Audit
{
    using AzureStorage.Application.Features.Audit.Queries;
    using FluentValidation;

    public  class GetAuditQueryByUniqueValidator : AbstractValidator<GetAuditQueryByUniqueQuery>
    {
        public GetAuditQueryByUniqueValidator()
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
