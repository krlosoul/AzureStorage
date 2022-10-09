namespace AzureStorage.api.Configurations
{
    using AzureStorage.Application;
    using AzureStorage.Infrastructure;

    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddInfrastructure();
            services.AddApplication();

            return services;
        }
    }
}
