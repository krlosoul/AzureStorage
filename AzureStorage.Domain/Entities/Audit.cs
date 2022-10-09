namespace AzureStorage.Domain.Entities
{
    using Microsoft.Azure.Cosmos.Table;

    public class Audit : TableEntity
    {
        public int TransactionTypeId { get; set; }
        public string? User { get; set; }
        public string? NewData { get; set; }
        public string? OldData { get; set; }
    }
}