using Microsoft.Azure.Documents.SystemFunctions;

namespace AzureStorage.Domain.Dtos
{
    public class CustomModelDto<T>
    {
        public string? ModelId { get; set; }
        public T? File { get; set; }
    }

    public class CustomFieldDto
    {
        public Dictionary<string, string> Fields { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, List<Dictionary<string, string>>> List { get; set; } = new Dictionary<string, List<Dictionary<string, string>>>();
    }
}