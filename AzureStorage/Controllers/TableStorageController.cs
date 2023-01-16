namespace AzureStorage.api.Controllers
{
    using AzureStorage.Application.Features.TableStorage.Commands;
    using AzureStorage.Application.Features.TableStorage.Queries;
    using AzureStorage.Domain.Dtos;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/V1/[controller]")]
    public class TableStorageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TableStorageController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// Get All Audits.
        /// </summary>
        /// <returns>IEnumerable&lt;AuditDto&gt;</returns>
        [HttpGet("GetAudit")]
        public Task<IEnumerable<AuditDto>> GetAudit() => _mediator.Send(new GetAuditQuery());

        /// <summary>
        /// Get Audit By Unique.
        /// </summary>
        /// <param name="deleteAuditCommand">The GetAuditQueryByUniqueDto.</param>
        /// <returns>AuditDto</returns>
        [HttpGet("GetAuditByUnique")]
        public Task<AuditDto> GetAuditByUnique([FromQuery] GetAuditQueryByUniqueQuery getAuditQueryByUnique) => _mediator.Send(getAuditQueryByUnique);

        /// <summary>
        /// Create Audit.
        /// </summary>
        /// <param name="postAuditCommand">The PostAuditDto.</param>
        /// <returns>AudtId;</returns>
        [HttpPost("CreateAudit")]
        public async Task<string> CreateAudit([FromBody] CreateAuditCommand createAuditCommand) => await _mediator.Send(createAuditCommand);

        /// <summary>
        /// Update Audit.
        /// </summary>
        /// <param name="postAuditCommand">The PostAuditDto.</param>
        /// <returns>AudtId;</returns>
        [HttpPut("UpdateAudit")]
        public async Task<string> UpdateAudit([FromBody] UpdateAuditCommand updateAuditCommand) => await _mediator.Send(updateAuditCommand);

        /// <summary>
        /// Delete Audit.
        /// </summary>
        /// <param name="deleteAuditCommand">The GetAuditQueryByUniqueDto.</param>
        /// <returns>AudtId;</returns>
        [HttpDelete("DeleteAudit")]
        public async Task<string> DeleteAudit([FromQuery] DeleteAuditCommand deleteAuditCommand) => await _mediator.Send(deleteAuditCommand);
    }
}
