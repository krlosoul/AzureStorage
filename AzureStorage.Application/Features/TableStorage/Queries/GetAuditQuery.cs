namespace AzureStorage.Application.Features.TableStorage.Queries
{
    using AzureStorage.Application.Common.Exceptions;
    using AzureStorage.Application.Contract;
    using AzureStorage.Domain.Dtos;
    using AzureStorage.Domain.Entities;
    using AutoMapper;
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using AzureStorage.Domain.Common.Enums;
    using Newtonsoft.Json;

    public class GetAuditQuery : IRequest<IEnumerable<AuditDto>> { }

    public class GetAuditQueryHandler : IRequestHandler<GetAuditQuery, IEnumerable<AuditDto>>
    {
        private readonly ITableStorageService<Audit> _repository;
        private readonly IMapper _mapper;

        public GetAuditQueryHandler(ITableStorageService<Audit> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AuditDto>> Handle(GetAuditQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Audit> audits = await _repository.GetAllAsync();
            if (!audits.Any())
            {
                throw new NoContentException();
            }
            IEnumerable<AuditDto> auditsDto = _mapper.Map<IEnumerable<Audit>, IEnumerable<AuditDto>>(audits);

            return auditsDto;
        }
    }
}