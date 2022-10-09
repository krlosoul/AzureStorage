namespace AzureStorage.api.Configurations
{
    public class SwaggerConfiguration
    {
        public SwaggerConfiguration(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("API AzureStorage", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "API MANAGER AzureStorage",
                    Version = "1"
                });
                List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
                xmlFiles.ForEach(xmlFile => options.IncludeXmlComments(xmlFile));
            });
        }
    }
}
