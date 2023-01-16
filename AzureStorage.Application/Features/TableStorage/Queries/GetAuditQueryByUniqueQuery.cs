namespace AzureStorage.Application.Features.TableStorage.Queries
{
    using AutoMapper;
    using AzureStorage.Application.Common.Exceptions;
    using AzureStorage.Application.Contract;
    using AzureStorage.Domain.Dtos;
    using AzureStorage.Domain.Entities;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetAuditQueryByUniqueQuery : GetAuditQueryByUniqueDto, IRequest<AuditDto>{}

    public class GetAuditQueryByUniqueQueryHandler : IRequestHandler<GetAuditQueryByUniqueQuery, AuditDto>
    {
        private readonly ITableStorageService<Audit> _repository;
        private readonly IMapper _mapper;

        public GetAuditQueryByUniqueQueryHandler(ITableStorageService<Audit> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AuditDto> Handle(GetAuditQueryByUniqueQuery request, CancellationToken cancellationToken)
        {
            Audit? audit = await _repository.FirstOrDefaultAsync(x => x.PartitionKey == request.Entity && x.RowKey == request.AuditId);
            if (audit == null)
            {
                throw new NotFoundException($"No found entity with entity {request.Entity} or AuditId {request.AuditId}");
            }
            AuditDto auditDto = _mapper.Map<Audit, AuditDto>(audit);

            return auditDto;
        }
    }
}
