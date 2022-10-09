namespace AzureStorage.Domain.Dtos
{
    public class AuditDto
    {
        public string? AuditId { get; set; }
        public string? Entity { get; set; }
        public string? TransactionName { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? User { get; set; }
        public string? NewData { get; set; }
        public string? OldData { get; set; }
    }

    public class CreateAuditDto
    {
        public string? Entity { get; set; }
        public int TransactionTypeId { get; set; }
        public string? User { get; set; }
        public string? NewData { get; set; }
        public string? OldData { get; set; }
    }

    public class UpdateAuditDto
    {
        public string? AuditId { get; set; }
        public string? Entity { get; set; }
        public int TransactionTypeId { get; set; }
        public string? User { get; set; }
        public string? NewData { get; set; }
        public string? OldData { get; set; }
    }

    public class GetAuditQueryByUniqueDto
    {
        public string? AuditId { get; set; }
        public string? Entity { get; set; }
    }
}
