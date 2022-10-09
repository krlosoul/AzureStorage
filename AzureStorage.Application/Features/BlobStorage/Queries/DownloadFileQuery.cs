namespace AzureStorage.Application.Features.BlobStorage.Queries
{
    using AzureStorage.Application.Common.Exceptions;
    using AzureStorage.Application.Contract;
    using AzureStorage.Domain.Dtos;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class DownloadFileQuery : GetBlobDto, IRequest<BlobDto> { }

    public class DownloadFileQueryHandler : IRequestHandler<DownloadFileQuery, BlobDto>
    {
        private readonly IBlobStorageService _repository;

        public DownloadFileQueryHandler(IBlobStorageService repository)
        {
            _repository = repository;
        }

        public async Task<BlobDto> Handle(DownloadFileQuery request, CancellationToken cancellationToken)
        {
            BlobDto blobDto = await _repository.DownloadFileAsync(request);
            if (string.IsNullOrEmpty(blobDto.Name))
            {
                throw new NoContentException();
            }

            return blobDto;
        }
    }
}
