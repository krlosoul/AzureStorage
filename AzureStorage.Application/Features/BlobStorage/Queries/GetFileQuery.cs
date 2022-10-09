namespace AzureStorage.Application.Features.BlobStorage.Queries
{
    using AzureStorage.Application.Common.Exceptions;
    using AzureStorage.Application.Contract;
    using AzureStorage.Domain.Dtos;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetFileQuery : GetBlobDto, IRequest<BlobDto> { }

    public class GetFileQueryHandler : IRequestHandler<GetFileQuery, BlobDto>
    {
        private readonly IBlobStorageService _repository;

        public GetFileQueryHandler(IBlobStorageService repository)
        {
            _repository = repository;
        }

        public async Task<BlobDto> Handle(GetFileQuery request, CancellationToken cancellationToken)
        {
            BlobDto blobDto = await _repository.GetFileAsync(request);
            if (string.IsNullOrEmpty(blobDto.Name))
            {
                throw new NoContentException();
            }

            return blobDto;
        }
    }
}
