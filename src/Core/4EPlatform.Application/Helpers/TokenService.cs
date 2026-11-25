using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace _4EPlatform.Application.Helpers
{
    public class TokenService : ITokenService
    {
        private IHttpContextAccessor _contextAccessor;
        private IConfiguration _configuration;

        public TokenService(IHttpContextAccessor contextAccessor, IConfiguration configuration)
        {
            _contextAccessor = contextAccessor;
            _configuration = configuration;
        }

        public async Task<CallOptions> AcquireToken()
        {

            string? jwtToken = _contextAccessor.HttpContext?.Request.Headers["Authorization"];
            if (string.IsNullOrWhiteSpace(jwtToken))
            {
                jwtToken = BackgroundAuthContext.Token;
            }

            string? acceptLanguage = _contextAccessor.HttpContext?.Request.Headers["Accept-Language"];
            if (string.IsNullOrWhiteSpace(acceptLanguage))
            {
                acceptLanguage = BackgroundAuthContext.AcceptLanguage;
            }


            if (string.IsNullOrWhiteSpace(jwtToken))
            {
                return new CallOptions();
            }

            if (jwtToken.StartsWith("Bearer "))
            {
                jwtToken = jwtToken.Substring("Bearer ".Length);
            }
            var metadata = new Metadata
        {
           { "Authorization", $"Bearer {jwtToken}" },
           {  "Accept-Language", string.IsNullOrWhiteSpace(acceptLanguage) ? "en" : acceptLanguage }
        };
            var callOptions = new CallOptions(metadata);
            return await System.Threading.Tasks.Task.FromResult(callOptions);
        }


    }
}
