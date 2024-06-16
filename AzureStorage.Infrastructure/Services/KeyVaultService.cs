namespace AzureStorage.Infrastructure.Services
{
    using Azure.Identity;
    using Azure.Security.KeyVault.Secrets;
    using Microsoft.Extensions.Configuration;
    using AzureStorage.Application.Contract;
    using AzureStorage.Domain.Dtos;
    using AzureStorage.Domain.Common.Constants;

    public class KeyVaultService : IKeyVaultService
    {
        #region Properties
        private readonly IConfiguration _configuration;
        private KeyVaultDto _keyVaultDto;
        private readonly SecretClient _client;
        #endregion

        public KeyVaultService(IConfiguration configuration)
        {
            _configuration = configuration;
            GetConfiguration();
            var credential = new ClientSecretCredential(_keyVaultDto.TenantId, _keyVaultDto.ClientId, _keyVaultDto.ClientSecret);
            _client = new SecretClient(new Uri(_keyVaultDto.Uri), credential);
        }

        public async Task<string> GetSecretAsync(string secretName)
        {
            try
            {
                var secret = await _client.GetSecretAsync(secretName);
                return secret.Value.Value;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region PrivateMethod
        private void GetConfiguration()
        {
            KeyVaultDto instance = _keyVaultDto = new KeyVaultDto();
            _configuration.Bind(AzureConstants.KeyVault, instance);
        }       
        #endregion
    }
}