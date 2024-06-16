namespace AzureStorage.Domain.Dtos
{
    public class GetSecretRequestDto
    {
        public string SecretName {get;set;}        
    }

    public class GetSecretResponseDto
    {
        public string SecretResult {get;set;}
    }
}