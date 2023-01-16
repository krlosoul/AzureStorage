namespace AzureStorage.Application.Features.FormRecognizer.Queries
{
    using AzureStorage.Application.Contract;
    using AzureStorage.Domain.Dtos;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using System.Threading;
    using System.Threading.Tasks;

    public class AnalyzeDocumentQuery : CustomModelDto<IFormFile>, IRequest<CustomFieldDto> { }

    public class AnalyzeDocumentQueryHandler : IRequestHandler<AnalyzeDocumentQuery, CustomFieldDto>
    {
        private readonly IFormRecognizerService _repository;

        public AnalyzeDocumentQueryHandler(IFormRecognizerService repository)
        {
            _repository = repository;
        }

        public async Task<CustomFieldDto> Handle(AnalyzeDocumentQuery request, CancellationToken cancellationToken)
        {
            CustomFieldDto customFields = await _repository.AnalyzeDocumentStreamAsync(request);

            return customFields;
        }
    }
}