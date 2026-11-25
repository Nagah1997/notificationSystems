using _4EPlatform.Domain.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Infrastrucure.Context
{
    public class ContextSeed
    {
        private static AppDBContext _dbContext;
        private static IServiceProvider _serviceProvider;
        public static void Seed(AppDBContext appDbContext, IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _dbContext = appDbContext;
            _dbContext.Database.EnsureCreated();
            _serviceProvider = serviceProvider;
            bool isSeedData = configuration.GetSection("IsSeedData").Get<bool>();
            if (isSeedData)
            {

            }
        }





    }
}
