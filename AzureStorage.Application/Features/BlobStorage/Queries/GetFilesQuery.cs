namespace AzureStorage.Application.Features.BlobStorage.Queries
{
    using AzureStorage.Application.Common.Exceptions;
    using AzureStorage.Application.Contract;
    using AzureStorage.Domain.Dtos;
    using MediatR;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetFilesQuery : GetBlobsDto, IRequest<IEnumerable<BlobDto>> { }

    public class GetFilesQueryHandler : IRequestHandler<GetFilesQuery, IEnumerable<BlobDto>>
    {
        private readonly IBlobStorageService _repository;

        public GetFilesQueryHandler(IBlobStorageService repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<BlobDto>> Handle(GetFilesQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<BlobDto> blobsDto = await _repository.GetFilesAsync(request);
            if (!blobsDto.Any())
            {
                throw new NoContentException();
            }

            return blobsDto;
        }
    }
}
