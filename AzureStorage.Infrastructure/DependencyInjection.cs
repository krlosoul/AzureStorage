namespace AzureStorage.Infrastructure
{
    using AzureStorage.Application.Contract;
    using AzureStorage.Infrastructure.Services;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient(typeof(ITableStorageService<>), typeof(TableStorageService<>));
            services.AddTransient(typeof(IBlobStorageService), typeof(BlobStorageService));

            return services;
        }
    }
}
