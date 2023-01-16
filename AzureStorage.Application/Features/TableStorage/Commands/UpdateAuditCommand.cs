namespace AzureStorage.Application.Features.TableStorage.Commands
{
    using AzureStorage.Application.Common.Exceptions;
    using AzureStorage.Application.Contract;
    using AzureStorage.Domain.Dtos;
    using AzureStorage.Domain.Entities;
    using MediatR;
    using Newtonsoft.Json;

    public class UpdateAuditCommand : UpdateAuditDto, IRequest<string> { }

    public class UpdateAuditCommandHandler : IRequestHandler<UpdateAuditCommand, string>
    {
        private readonly ITableStorageService<Audit> _repository;

        public UpdateAuditCommandHandler(ITableStorageService<Audit> repository)
        {
            _repository = repository;
        }

        public async Task<string> Handle(UpdateAuditCommand request, CancellationToken cancellationToken)
        {
            Audit? audit = await _repository.FirstOrDefaultAsync(x => x.PartitionKey == request.Entity && x.RowKey == request.AuditId);
            if (audit == null)
            {
                throw new NotFoundException($"No found entity with entity {request.Entity} or AuditId {request.AuditId}");
            }
            audit.TransactionTypeId = request.TransactionTypeId;
            audit.User = request.User;
            audit.NewData = JsonConvert.SerializeObject(request.NewData);
            audit.OldData = JsonConvert.SerializeObject(request.OldData);
            string eTag = await _repository.UpdateAsync(audit);

            return eTag;
        }
    }
}
