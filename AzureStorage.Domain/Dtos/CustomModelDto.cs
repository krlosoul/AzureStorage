namespace AzureStorage.Domain.Dtos
{
    public class CustomModelDto<T>
    {
        public string? ModelId { get; set; }
        public T? File { get; set; }
    }

    public class CustomFieldDto<T>
    {
        public string? Key { get; set; }
        public T? Value { get; set; }
    }

    public class CustomFieldDto
    {
        public IList<CustomFieldDto<string>>? Fields { get; set; }
        public List<CustomFieldDto<List<List<CustomFieldDto<string>>>>>? List { get; set; }
    }
}