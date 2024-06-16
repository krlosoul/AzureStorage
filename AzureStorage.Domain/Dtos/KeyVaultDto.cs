namespace AzureStorage.Domain.Dtos
{
    public class KeyVaultDto
    {
        public string Uri { get; set; }
        public string TenantId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}