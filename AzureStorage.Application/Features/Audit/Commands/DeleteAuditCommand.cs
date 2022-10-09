namespace AzureStorage.Application.Features.Audit.Commands
{
    using AzureStorage.Application.Common.Exceptions;
    using AzureStorage.Application.Contract;
    using AzureStorage.Domain.Dtos;
    using AzureStorage.Domain.Entities;
    using MediatR;

    public class DeleteAuditCommand : GetAuditQueryByUniqueDto, IRequest<string> { }

    public class DeleteAuditCommandHandler : IRequestHandler<DeleteAuditCommand, string>
    {
        private readonly ITableStorageService<Audit> _repository;

        public DeleteAuditCommandHandler(ITableStorageService<Audit> repository)
        {
            _repository = repository;
        }

        public async Task<string> Handle(DeleteAuditCommand request, CancellationToken cancellationToken)
        {
            Audit? audit = await _repository.FirstOrDefaultAsync(x => x.PartitionKey == request.Entity && x.RowKey == request.AuditId);
            if (audit == null)
            {
                throw new NotFoundException($"No found entity with entity {request.Entity} or AuditId {request.AuditId}");
            }
            string eTag = await _repository.DeleteAsync(audit);

            return eTag;
        }
    }
}
