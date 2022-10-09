namespace AzureStorage.Application.Features.BlobStorage.Commands
{
    using AzureStorage.Application.Common.Exceptions;
    using AzureStorage.Application.Contract;
    using AzureStorage.Domain.Dtos;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class DeleteFileCommand : GetBlobDto, IRequest<BlobDto> { }

    public class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand, BlobDto>
    {
        private readonly IBlobStorageService _repository;

        public DeleteFileCommandHandler(IBlobStorageService repository)
        {
            _repository = repository;
        }

        public async Task<BlobDto> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            BlobDto blobDto = await _repository.DeleteFileAsync(request);
            if (string.IsNullOrEmpty(blobDto.Name))
            {
                throw new NoContentException();
            }

            return blobDto;
        }
    }
}
