
using Microsoft.AspNetCore.Diagnostics;

namespace _4EPlatform_API_Task.MiddleWares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        private readonly IHostEnvironment _host;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger,
                IHostEnvironment host, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _logger = logger;
            _host = host;
            _sharedLocalizer = sharedLocalizer;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError($"Error occurred while processing the request Message:{exception.Message}, StackTrace:{exception.StackTrace}");

            var errorKey = string.IsNullOrWhiteSpace(exception?.Message)
                ? "AnErrorOccurredPleaseContactSystemAdministrator"
                : exception.Message;

            var translatedErrors = errorKey
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(key => _sharedLocalizer[key.Trim()].Value)
                .ToArray();

            var errorList = new ErrorList(translatedErrors);
            var result = Result.Error(errorList);

            // ── تحديد الـ Status Code ──────────────────────────────────────
            httpContext.Response.StatusCode = exception switch
            {
                InvalidOperationException => StatusCodes.Status400BadRequest,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                _ => StatusCodes.Status500InternalServerError
            };

            var serializedResponse = JsonSerializer.Serialize(result, new JsonSerializerOptions
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsync(serializedResponse);

            return true;
        }
    }
}
