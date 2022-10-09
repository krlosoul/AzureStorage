namespace AzureStorage.Application.Validators.BlobStorage
{
    using AzureStorage.Application.Features.BlobStorage.Queries;
    using FluentValidation;

    public class GetFilesValidator : AbstractValidator<GetFilesQuery>
    {
        public GetFilesValidator()
        {
            RuleFor(r => r.ContainerName)
                .NotEmpty()
                .WithMessage("The 'ContainerName' is required.");
        }
    }
}
