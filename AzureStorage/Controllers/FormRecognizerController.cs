namespace AzureStorage.Api.Controllers
{
    using AzureStorage.Application.Features.FormRecognizer.Queries;
    using AzureStorage.Domain.Dtos;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/V1/[controller]")]
    public class FormRecognizerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FormRecognizerController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// Analyze Document.
        /// </summary>
        /// <param name="analyzeDocumentQuery">The AnalyzeDocumentQuery.</param>
        /// <returns>&lt;List&lt;CustomFieldDto&gt;&gt;.</returns>
        [HttpPost("AnalyzeDocument")]
        public Task<CustomFieldDto> AnalyzeDocument([FromForm] AnalyzeDocumentQuery analyzeDocumentQuery) => _mediator.Send(analyzeDocumentQuery);
    }
}
