using Infrastrucure.Context;

namespace _4EPlatform_API_Template.Extention
{
    public static class DBContextService
    {
        public static void DatatBaseContext(this IServiceCollection services, WebApplicationBuilder config, bool IsDevelopment)
        {
            services.AddDbContext<AppDBContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(config.Configuration.GetConnectionString("DefaultConnection"));
           options.EnableSensitiveDataLogging(IsDevelopment);
            });
        }
    }
}
