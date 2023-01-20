namespace AzureStorage.Infrastructure.Services
{
    using Azure;
    using Azure.AI.FormRecognizer.DocumentAnalysis;
    using AzureStorage.Application.Contract;
    using AzureStorage.Domain.Common.Constants;
    using AzureStorage.Domain.Dtos;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using System.IO;
    using System.Threading.Tasks;

    public class FormRecognizerService : IFormRecognizerService
    {
        #region Properties
        private readonly IConfiguration _configuration;
        private CognitiveServicesDto? _connectionDto;
        private readonly DocumentAnalysisClient _documentAnalysisClient;
        #endregion

        /// <summary>
        /// Initializes a new instance of the FormRecognizerService.
        /// </summary>
        /// /// <param name="configuration">The IConfiguration.</param>
        public FormRecognizerService(IConfiguration configuration)
        {
            _configuration = configuration;
            GetConfiguration();
            AzureKeyCredential credential = new(_connectionDto?.Key);
            Uri uri = new(_connectionDto?.EndPoint);
            _documentAnalysisClient = new DocumentAnalysisClient(uri, credential);
        }

        public async Task<CustomFieldDto> AnalyzeDocumentStreamAsync(CustomModelDto<IFormFile> customModelDto)
        {
            await using Stream? data = customModelDto.File?.OpenReadStream();
            AnalyzeDocumentOperation operation = await _documentAnalysisClient.AnalyzeDocumentAsync(WaitUntil.Completed, customModelDto.ModelId, data);

            return AnalyzeResult(operation.Value);
        }

        public async Task<CustomFieldDto> AnalyzeDocumentUriAsync(CustomModelDto<Uri> customModelDto)
        {
            AnalyzeDocumentOperation operation = await _documentAnalysisClient.AnalyzeDocumentFromUriAsync(WaitUntil.Completed, customModelDto.ModelId, customModelDto.File);

            return AnalyzeResult(operation.Value);
        }

        #region PrivateMethod
        /// <summary>
        /// Binding configuration.
        /// </summary>
        private void GetConfiguration()
        {
            CognitiveServicesDto instance = _connectionDto = new CognitiveServicesDto();
            _configuration.Bind(AzureConstants.CognitiveServices, instance);
        }

        /// <summary>
        /// Get Result from AnalyzeDocument.
        /// </summary>
        /// <param name="analyzeResult">The AnalyzeResult.</param>
        /// <returns>&lt;List&lt;CustomFieldDto&gt;&gt;.</returns>
        private static CustomFieldDto AnalyzeResult(AnalyzeResult analyzeResult)
        {
            CustomFieldDto customFieldDto = new();
            for (int i = 0; i < analyzeResult.Documents.Count; i++)
            {
                AnalyzedDocument document = analyzeResult.Documents[i];
                IEnumerable<string> keys = document.Fields.Keys;
                foreach (string key in keys)
                {
                    string field = FieldResult(key, document.Fields);
                    List<Dictionary<string, string>> group = FieldsResult(key, document.Fields);
                    if (field.Any())
                        customFieldDto.Fields.Add(key,field);
                    if (group.Any())
                        customFieldDto.List.Add(key, group);
                }
            }

            return customFieldDto;
        }

        /// <summary>
        /// Get field result.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="document">The AnalyzedDocument.</param>
        /// <returns>&lt;string&gt;.</returns>
        private static string FieldResult(string key, IReadOnlyDictionary<string, DocumentField> fields)
        {
            string value = string.Empty;
            if (fields.TryGetValue(key, out DocumentField? documentField) && documentField.FieldType == DocumentFieldType.String)
            {
                value = documentField.Value.AsString();
            }

            return value;
        }

        /// <summary>
        /// Get fields result.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="document">The AnalyzedDocument.</param>
        /// <returns>&lt;List&lt;Dictionary&lt;string,string&gt;&gt;&gt;.</returns>
        private static List<Dictionary<string, string>> FieldsResult(string key, IReadOnlyDictionary<string, DocumentField> fields)
        {
            List<Dictionary<string, string>> group = new();
            if (fields.TryGetValue(key, out DocumentField? documentField) && documentField.FieldType == DocumentFieldType.List)
            {
                foreach (DocumentField itemField in documentField.Value.AsList())
                {
                    if (itemField.FieldType == DocumentFieldType.Dictionary)
                    {
                        IReadOnlyDictionary<string, DocumentField> itemFields = itemField.Value.AsDictionary();
                        Dictionary<string, string> field = new Dictionary<string, string>();
                        foreach (string subKey in itemFields.Keys)
                        {
                            field.Add(subKey, FieldResult(subKey, itemFields));
                        }
                        group.Add(field);
                    }
                }
            }

            return group;
        }
        #endregion
    }
}