﻿namespace AzureStorage.Infrastructure
{
    using AzureStorage.Application.Contract;
    using AzureStorage.Infrastructure.Services;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IKeyVaultService, KeyVaultService>();
            services.AddTransient(typeof(ITableStorageService<>), typeof(TableStorageService<>));
            services.AddTransient(typeof(IBlobStorageService), typeof(BlobStorageService));
            services.AddTransient(typeof(IFormRecognizerService), typeof(FormRecognizerService));

            return services;
        }
    }
}
