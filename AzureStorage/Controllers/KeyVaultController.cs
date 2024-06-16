namespace AzureStorage.Controllers
{
    using System.Threading.Tasks;
    using AzureStorage.Application.Features.KeyVault.Queries;
    using AzureStorage.Domain.Dtos;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class KeyVaultController : ControllerBase
    {
        private readonly IMediator _mediator;

        public KeyVaultController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// Get secret by name.
        /// </summary>
        /// <param name="getSecretQuery">The KeyVault.</param>
        /// <returns>secret.</returns>
        [HttpGet("GetSecret")]
        public Task<GetSecretResponseDto> GetAuditByUnique([FromQuery] GetSecretQuery getSecretQuery) => _mediator.Send(getSecretQuery);
    }
}