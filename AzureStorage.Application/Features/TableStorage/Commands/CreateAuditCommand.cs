namespace AzureStorage.Application.Features.TableStorage.Commands
{
    using AzureStorage.Application.Contract;
    using AzureStorage.Domain.Dtos;
    using AzureStorage.Domain.Entities;
    using AutoMapper;
    using MediatR;
    using System;
    using System.Threading.Tasks;

    public class CreateAuditCommand : CreateAuditDto, IRequest<string> { }

    public class CreateAuditCommandHandler : IRequestHandler<CreateAuditCommand, string>
    {
        private readonly ITableStorageService<Audit> _repository;
        private readonly IMapper _mapper;

        public CreateAuditCommandHandler(ITableStorageService<Audit> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<string> Handle(CreateAuditCommand request, CancellationToken cancellationToken)
        {
            string guid = Guid.NewGuid().ToString();
            Audit audit = _mapper.Map<CreateAuditDto, Audit>(request);
            audit.RowKey = guid;            
            string eTag = await _repository.InsertAsync(audit);

            return eTag;
        }
    }
}