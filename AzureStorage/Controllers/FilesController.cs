namespace AzureStorage.Api.Controllers
{
    using AzureStorage.Application.Features.BlobStorage.Commands;
    using AzureStorage.Application.Features.BlobStorage.Queries;
    using AzureStorage.Domain.Dtos;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/V1/[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FilesController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// Get all files in container.
        /// </summary>
        /// <param name="getFilesQuery">The GetBlobsDto.</param>
        /// <returns>IEnumerable&lt;BlobDto&gt;.</returns>
        [HttpGet("GetFiles")]
        public Task<IEnumerable<BlobDto>> GetFiles([FromQuery] GetFilesQuery getFilesQuery) => _mediator.Send(getFilesQuery);

        /// <summary>
        /// Get file in container.
        /// </summary>
        /// <param name="getBlobDto">The GetBlobDto.</param>
        /// <returns>&lt;BlobDto&gt;.</returns>
        [HttpGet("GetFile")]
        public Task<BlobDto> GetFile([FromQuery] GetFileQuery getFileQuery) => _mediator.Send(getFileQuery);

        /// <summary>
        /// Download a file with filename
        /// </summary>
        /// <param name="downloadFileQuery">The GetBlobDto.</param>
        /// <returns>&lt;File&gt;.</returns>
        [HttpGet("DownloadFile")]
        public async Task<IActionResult> DownloadFile([FromQuery] DownloadFileQuery downloadFileQuery)
        {
            BlobDto blobDto = await _mediator.Send(downloadFileQuery);

            return File(blobDto.Content, blobDto.ContentType, blobDto.Name);
        }

        /// <summary>
        /// Upload a file.
        /// </summary>
        /// <param name="uploadFileCommand">The UploadBlobDto.</param>
        /// <returns>&lt;BlobDto&gt;.</returns>
        [HttpPost("UploadFile")]
        public Task<BlobDto> UploadFile([FromForm] UploadFileCommand uploadFileCommand) => _mediator.Send(uploadFileCommand);

        /// <summary>
        /// Deleted a file with filename.
        /// </summary>
        /// <param name="DeleteFileCommand">The GetBlobDto.</param>
        /// <returns>&lt;BlobDto&gt;.</returns>
        [HttpDelete("DeleteFile")]
        public Task<BlobDto> DeleteFile([FromQuery] DeleteFileCommand deleteFileCommand) => _mediator.Send(deleteFileCommand);
    }
}
