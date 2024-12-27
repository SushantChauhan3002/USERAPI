namespace CleanArchitecture.API.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string ApiKeyHeaderName = "X-API-KEY";
        private readonly ILogger<ApiKeyMiddleware> _logger;

        public ApiKeyMiddleware(RequestDelegate next, ILogger<ApiKeyMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Check if the API key is provided in the request header

            if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var apiKeyVal))
            {
                context.Response.StatusCode = 401;     // Unauthorized

                await context.Response.WriteAsync("API Key not found!");

                return;
            }

            // Retrieve the API key from the app settings

            var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();

            var apiKey = appSettings.GetValue<string>("AppSettings:ApiKey");

            // Log the value of apiKey to ensure it's being loaded correctly

            _logger.LogInformation("Retrieved API Key from appsettings: {ApiKey}", apiKey);

            // Check if the API key from settings is null or empty

            if (string.IsNullOrEmpty(apiKey))
            {
                context.Response.StatusCode = 500; // Internal Server Error

                await context.Response.WriteAsync("API Key configuration is missing in the app settings.");

                return;
            }

            // Compare the provided API key with the expected API key

            if (!apiKey.Equals(apiKeyVal))
            {
                context.Response.StatusCode = 401; // Unauthorized

                await context.Response.WriteAsync("Unauthorized client");

                return;
            }

            // If the API key is valid, proceed to the next middleware in the pipeline

            await _next(context);
        }
    }
}