
using _4EPlatform.Application.ClientGrpc.Lookup;
using _4EPlatform.Application.ClientGrpc.User;
using _4EPlatform.Application.IClientGrpc.Lookup;
using _4EPlatform.Application.IClientGrpc.NotificationClient;
using _4EPlatform.Application.IClientGrpc.User;

using _4EPlatform_API_Task.MiddleWares;

namespace _4EPlatform_API_Template.Extention
{
    public static class ServiceExtension
    {

        public static void AddMapGrpcServicesApp(this WebApplication app)
        {



        }

        public static void AddUnitOfWorkRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
  
            services.AddScoped(typeof(IGRepository<>), typeof(GRepository<>));
 
            services.AddScoped<ILookupClient, LookupClient>();
 
            services.AddScoped<IUserClient, UserClient>();
            services.AddHttpContextAccessor();
        }
        public static void AddDiServices(this IServiceCollection services)
        {
             //services.AddAutoMapper(AppDomain.CurrentDomain.Load("4EPlatform.Application"));

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(ApplicationLayer).Assembly);
            });

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            //services.AddAutoMapper(typeof(ApplicationAssemblyMarker).Assembly);
        //    services.AddAutoMapper(typeof(ApplicationLayer).Assembly);

            services.AddScoped<GlobalExceptionHandler, GlobalExceptionHandler>();
            services.AddSingleton<ITokenService, TokenService>();

        }

        public static void AddRateLimitings(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRateLimiter(options =>
            {
                var rateLimitSection = configuration.GetSection("RateLimit");
                var permitLimit = rateLimitSection.GetValue<int>("PermitLimit");
                var queueLimit = rateLimitSection.GetValue<int>("QueueLimit");
                var windowSeconds = rateLimitSection.GetValue<int>("WindowSeconds");
                options.AddFixedWindowLimiter("FixedWindowPolicy", opt =>
                {
                    opt.Window = TimeSpan.FromSeconds(windowSeconds);
                    opt.PermitLimit = permitLimit;
                    opt.QueueLimit = queueLimit;
                    opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                }).RejectionStatusCode = 429;
            });
        }

        public static void AddVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning().AddApiExplorer(options =>
            {
                // Add the versioned API explorer, which also adds IApiVersionDescriptionProvider service
                // note: the specified format code will format the version as "'v'major[.minor][-status]"
                options.GroupNameFormat = "'v'VVV";

                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                // can also be used to control the format of the API version in route templates
            });
        }
        public static void AddLocalizationOptions(this IServiceCollection services)
        {
            #region Localization
            services.AddLocalization(opt =>
            {
                opt.ResourcesPath = "";
            });

            services.Configure<RequestLocalizationOptions>(options =>
            {
                List<CultureInfo> supportedCultures = new List<CultureInfo>{
            new CultureInfo("en-US"),
            new CultureInfo("de-DE"),
            new CultureInfo("fr-FR"),
            new CultureInfo("ar")};

                options.DefaultRequestCulture = new RequestCulture("ar");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            #endregion
        }
    }
}
