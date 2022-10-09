namespace AzureStorage.Application.Features.BlobStorage.Commands
{
    using AzureStorage.Application.Contract;
    using AzureStorage.Domain.Dtos;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using System.Threading;
    using System.Threading.Tasks;

    public class UploadFileCommand : UploadBlobDto<IFormFile>, IRequest<BlobDto> { }

    public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, BlobDto>
    {
        private readonly IBlobStorageService _repository;

        public UploadFileCommandHandler(IBlobStorageService repository)
        {
            _repository = repository;
        }

        public async Task<BlobDto> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            BlobDto blobDto = await _repository.UploadFileAsync(request);

            return blobDto;
        }
    }
}
