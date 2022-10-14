namespace AzureStorage.Domain.Dtos
{
    public class AuditDto
    {
        public string? AuditId { get; set; }
        public string? Entity { get; set; }
        public string? TransactionName { get; set; }
        public string? TransactionDate { get; set; }
        public string? User { get; set; }
        public IEnumerable<DataDto>? NewData { get; set; }
        public IEnumerable<DataDto>? OldData { get; set; }
    }

    public class CreateAuditDto
    {
        public string? Entity { get; set; }
        public int TransactionTypeId { get; set; }
        public string? User { get; set; }
        public IEnumerable<DataDto>? NewData { get; set; }
        public IEnumerable<DataDto>? OldData { get; set; }
    }

    public class UpdateAuditDto
    {
        public string? AuditId { get; set; }
        public string? Entity { get; set; }
        public int TransactionTypeId { get; set; }
        public string? User { get; set; }
        public IEnumerable<DataDto>? NewData { get; set; }
        public IEnumerable<DataDto>? OldData { get; set; }
    }

    public class GetAuditQueryByUniqueDto
    {
        public string? AuditId { get; set; }
        public string? Entity { get; set; }
    }
}
