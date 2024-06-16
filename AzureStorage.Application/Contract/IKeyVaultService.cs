namespace AzureStorage.Application.Contract
{
    using System.Threading.Tasks;
    
    public interface IKeyVaultService
    {
        /// <summary>
        /// Get secret by name
        /// </summary>
        /// <param name="secretName">The secret name</param>
        /// <returns>secret</returns>
        Task<string> GetSecretAsync(string secretName);
    }
}