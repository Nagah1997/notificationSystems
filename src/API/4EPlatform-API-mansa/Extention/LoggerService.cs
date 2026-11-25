namespace _4EPlatform_Transactions_API.Extention
{
    public static class LoggerService
    {
        public static void Logger(this WebApplicationBuilder services, WebApplicationBuilder config)
        {
            Log.Logger = new LoggerConfiguration()
                   .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://elasticsearch:9200"))
                   {
                       AutoRegisterTemplate = true,
                       AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6
                   }).CreateLogger();
            services.Host.UseSerilog();
            var logger = new LoggerConfiguration()
          .ReadFrom.Configuration(config.Configuration)
          .Enrich.FromLogContext()
          .CreateLogger();
            services.Host.UseSerilog(logger);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

        }
    }
}
