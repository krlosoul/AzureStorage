namespace AzureStorage.Application.Validators.BlobStorage
{
    using AzureStorage.Application.Features.BlobStorage.Commands;
    using FluentValidation;

    public class DeleteFileValidator : AbstractValidator<DeleteFileCommand>
    {
        public DeleteFileValidator()
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
