namespace AzureStorage.Domain.Dtos
{
    public class BlobDto
    {
        public string? Uri { get; set; }
        public string? Name { get; set; }
        public string? ContentType { get; set; }
        public Stream? Content { get; set; }
    }

    public class GetBlobsDto
    {
        public string? ContainerName { get; set; }
    }

    public class UploadBlobDto<T>
    {
        public string? ContainerName { get; set; }
        public T? File { get; set; }
    }

    public class GetBlobDto
    {
        public string? ContainerName { get; set; }
        public string? FileName { get; set; }
    }
}
