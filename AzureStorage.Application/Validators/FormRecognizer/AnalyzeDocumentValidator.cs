namespace AzureStorage.Application.Validators.FormRecognizer
{
    using AzureStorage.Application.Features.FormRecognizer.Queries;
    using FluentValidation;

    public class AnalyzeDocumentValidator : AbstractValidator<AnalyzeDocumentQuery>
    {
        public AnalyzeDocumentValidator()
        {
            RuleFor(r => r.ModelId)
                    .NotEmpty()
                    .WithMessage("The 'ModelId' is required.");

            RuleFor(r => r.File)
                .NotEmpty()
                .WithMessage("The 'File' is required.");
        }
    }
}
