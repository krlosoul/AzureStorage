namespace AzureStorage.Application.Features.KeyVault.Queries
{
    using System.Threading;
    using System.Threading.Tasks;
    using AzureStorage.Application.Contract;
    using AzureStorage.Domain.Dtos;
    using MediatR;

    public class GetSecretQuery: GetSecretRequestDto, IRequest<GetSecretResponseDto>{}

    public class GetSecretQueryHandler : IRequestHandler<GetSecretQuery, GetSecretResponseDto>
    {
        private readonly IKeyVaultService _repository;

        public GetSecretQueryHandler(IKeyVaultService repository)
        {
            _repository = repository;
        }

        public async Task<GetSecretResponseDto> Handle(GetSecretQuery request, CancellationToken cancellationToken)
        {
            var secret = await _repository.GetSecretAsync(request.SecretName);
            GetSecretResponseDto getSecretResponseDto = new(){SecretResult = secret};
            return getSecretResponseDto;
        }
    }
}