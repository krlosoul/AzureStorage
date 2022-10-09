namespace AzureStorage.Application.Validators.BlobStorage
{
    using AzureStorage.Application.Features.BlobStorage.Commands;
    using FluentValidation;

    public class UploadFileValidator : AbstractValidator<UploadFileCommand>
    {
        public UploadFileValidator()
        {
            RuleFor(r => r.ContainerName)
                .NotEmpty()
                .WithMessage("The 'ContainerName' is required.");

            RuleFor(r => r.File)
                .NotEmpty()
                .WithMessage("The 'File' is required.");
        }
    }
}
