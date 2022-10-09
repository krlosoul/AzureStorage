namespace AzureStorage.Application.Contract
{
    using AzureStorage.Domain.Dtos;
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBlobStorageService
    {
        #region Queries
        /// <summary>
        /// Get all files in container.
        /// </summary>
        /// <param name="getBlobsDto">The GetBlobsDto.</param>
        /// <returns>IEnumerable&lt;BlobDto&gt;.</returns>
        Task<IEnumerable<BlobDto>> GetFilesAsync(GetBlobsDto getBlobsDto);

        /// <summary>
        /// Get file in container.
        /// </summary>
        /// <param name="getBlobDto">The GetBlobDto.</param>
        /// <returns>&lt;BlobDto&gt;.</returns>
        Task<BlobDto> GetFileAsync(GetBlobDto getBlobDto);

        /// <summary>
        /// Download a file with filename
        /// </summary>
        /// <param name="getBlobDto">The GetBlobDto.</param>
        /// <returns>&lt;BlobDto&gt;.</returns>
        Task<BlobDto> DownloadFileAsync(GetBlobDto getBlobDto);
        #endregion

        #region Commands
        /// <summary>
        /// Upload a file.
        /// </summary>
        /// <param name="uploadBlobDto">The UploadBlobDto&lt;IFormFile&gt;.</param>
        /// <param name="file">The file.</param>
        /// <returns>&lt;BlobDto&gt;.</returns>
        Task<BlobDto> UploadFileAsync(UploadBlobDto<IFormFile> uploadBlobDto);        

        /// <summary>
        /// Deleted a file with filename.
        /// </summary>
        /// <param name="getBlobDto">The GetBlobDto.</param>
        /// <returns>&lt;BlobDto&gt;.</returns>
        Task<BlobDto> DeleteFileAsync(GetBlobDto getBlobDto);
        #endregion
    }
}
