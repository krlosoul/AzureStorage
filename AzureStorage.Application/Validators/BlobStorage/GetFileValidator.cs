namespace AzureStorage.Application.Validators.BlobStorage
{
    using AzureStorage.Application.Features.BlobStorage.Queries;
    using FluentValidation;

    public class GetFileValidator : AbstractValidator<GetFileQuery>
    {
        public GetFileValidator()
        {
            RuleFor(r => r.ContainerName)
                .NotEmpty()
                .WithMessage("The 'ContainerName' is required.");

            RuleFor(r => r.FileName)
                .NotEmpty()
                .WithMessage("The 'FileName' is required.");
        }
    }
}
