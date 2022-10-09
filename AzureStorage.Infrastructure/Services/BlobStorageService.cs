namespace AzureStorage.Infrastructure.Services
{
    using Azure.Storage.Blobs;
    using Azure.Storage.Blobs.Models;
    using Azure.Storage.Sas;
    using AzureStorage.Application.Contract;
    using AzureStorage.Domain.Common.Constants;
    using AzureStorage.Domain.Dtos;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    public class BlobStorageService : IBlobStorageService
    {
        #region Properties
        private readonly IConfiguration _configuration;
        private ConnectionDto? _connectionDto;
        private readonly BlobServiceClient _blobServiceClient;
        #endregion

        /// <summary>
        /// Initializes a new instance of the AzureBlobStorage.
        /// </summary>
        /// /// <param name="configuration">The IConfiguration.</param>
        public BlobStorageService(IConfiguration configuration)
        {
            _configuration = configuration;
            GetConfiguration();
            _blobServiceClient = new(_connectionDto?.ConnectionString);
        }

        #region Queries
        public async Task<IEnumerable<BlobDto>> GetFilesAsync(GetBlobsDto getBlobsDto)
        {
            IList<BlobDto> files = new List<BlobDto>();
            BlobContainerClient container = GetContainer(getBlobsDto.ContainerName);
            string sas = GetSasUriForContainer(container);
            await foreach (BlobItem file in container.GetBlobsAsync())
            {
                string uri = container.Uri.ToString();
                string name = file.Name;
                string fullUri = $"{uri}/{name}{sas}";
                files.Add(new BlobDto
                {
                    Uri = fullUri,
                    Name = name,
                    ContentType = file.Properties.ContentType
                });
            }

            return files;
        }

        public async Task<BlobDto> GetFileAsync(GetBlobDto getBlobDto)
        {
            BlobDto blobDto = new();
            BlobContainerClient container = GetContainer(getBlobDto.ContainerName);
            string sas = GetSasUriForContainer(container);
            BlobClient file = container.GetBlobClient(getBlobDto.FileName);
            if (await file.ExistsAsync())
            {
                blobDto.Name = file.Name;
                blobDto.Uri = $"{file.Uri}{sas}";
            }

            return blobDto;
        }

        public async Task<BlobDto> DownloadFileAsync(GetBlobDto getBlobDto)
        {
            BlobDto blobDto = new();
            BlobContainerClient container = GetContainer(getBlobDto.ContainerName);
            BlobClient file = container.GetBlobClient(getBlobDto.FileName);
            if (await file.ExistsAsync())
            {
                Stream data = await file.OpenReadAsync();
                Stream blobContent = data;
                Azure.Response<BlobDownloadResult> content = await file.DownloadContentAsync();
                string? name = getBlobDto.FileName;
                string contentType = content.Value.Details.ContentType;
                blobDto = new() { Content = blobContent, Name = name, ContentType = contentType };
            }
            return blobDto;
        }
        #endregion

        #region Commands
        public async Task<BlobDto> UploadFileAsync(UploadBlobDto<IFormFile> uploadBlobDto)
        {
            BlobContainerClient container = GetContainer(uploadBlobDto.ContainerName);
            BlobClient client = container.GetBlobClient(uploadBlobDto.File?.FileName);
            await using (Stream? data = uploadBlobDto.File?.OpenReadStream())
            {
                await client.UploadAsync(data);
            }
            BlobDto blobDto = new() { Uri = client.Uri.AbsoluteUri, Name = client.Name };

            return blobDto;
        }

        public async Task<BlobDto> DeleteFileAsync(GetBlobDto getBlobDto)
        {
            BlobContainerClient container = GetContainer(getBlobDto.ContainerName);
            BlobClient file = container.GetBlobClient(getBlobDto.FileName);
            await file.DeleteAsync();
            BlobDto blobDto = new() { Name = getBlobDto.FileName };

            return blobDto;
        }
        #endregion

        #region PrivateMethod
        private void GetConfiguration() => _configuration.Bind(AzureConstants.AzureStorage, _connectionDto = new ConnectionDto());

        private BlobContainerClient GetContainer(string? containerName)
        {
            BlobContainerClient container = _blobServiceClient.GetBlobContainerClient(containerName);
            container.CreateIfNotExists();

            return container;
        }

        private static string GetSasUriForContainer(BlobContainerClient containerClient)
        {
            if (containerClient.CanGenerateSasUri)
            {
                BlobSasBuilder sasBuilder = new()
                {
                    BlobContainerName = containerClient.Name,
                    Resource = "c",
                    ExpiresOn = DateTimeOffset.UtcNow.AddHours(1)
                };
                sasBuilder.SetPermissions(BlobContainerSasPermissions.Read);
                Uri sasUri = containerClient.GenerateSasUri(sasBuilder);

                return sasUri.Query.ToString();
            }
            else
            {
                return String.Empty;
            }
        }
        #endregion
    }
}
