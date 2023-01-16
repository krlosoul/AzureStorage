namespace AzureStorage.Application.Contract
{
    using AzureStorage.Domain.Dtos;
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;

    public interface IFormRecognizerService
    {
        /// <summary>
        /// Analyze Document.
        /// </summary>
        /// <param name="customModelDto">The CustomModelDto&lt;IFormFile&gt;.</param>
        /// <returns>&lt;List&lt;CustomFieldDto&gt;&gt;.</returns>
        Task<CustomFieldDto> AnalyzeDocumentStreamAsync(CustomModelDto<IFormFile> customModelDto);

        /// <summary>
        /// Analyze Document By Uri.
        /// </summary>
        /// <param name="customModelDto">The CustomModelDto&lt;Uri&gt;.</param>
        /// <returns>&lt;List&lt;CustomFieldDto&gt;&gt;.</returns>
        Task<CustomFieldDto> AnalyzeDocumentUriAsync(CustomModelDto<Uri> customModelDto);
    }
}
