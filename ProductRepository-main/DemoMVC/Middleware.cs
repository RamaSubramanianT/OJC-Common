
using System.Text;

namespace DemoMVC
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class Middleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public Middleware(RequestDelegate next,ILoggerFactory logFactory)
        {
            _next = next;
            _logger = logFactory.CreateLogger("Middleware 1");
        }

        public async Task Invoke(HttpContext httpContext)
        {
            //await LogRequest(httpContext);
            _logger.LogInformation("Middleware execution");

            /*var response = httpContext.Response.Body;
            using (var responseBody = new MemoryStream())
            {
                httpContext.Response.Body = responseBody;
                //await _next(httpContext);
            }
            httpContext.Response.OnStarting(() =>
            {
                httpContext.Response.Headers["Message"] = $"Developed by internal team";
                return Task.CompletedTask;
            });*/
            await _next(httpContext);
        }
        private async Task LogResponse(HttpContext context, MemoryStream responseBody, Stream originalResponseBody)
        {
            var responseContent = new StringBuilder();
            responseContent.AppendLine("=== Response Info ===");

            responseContent.AppendLine("-- headers");
            foreach (var (headerKey, headerValue) in context.Response.Headers)
            {
                responseContent.AppendLine($"header = {headerKey}    value = {headerValue}");
            }

            responseContent.AppendLine("-- body");
            responseBody.Position = 0;
            var content = await new StreamReader(responseBody).ReadToEndAsync();
            responseContent.AppendLine($"body = {content}");
            responseBody.Position = 0;
            await responseBody.CopyToAsync(originalResponseBody);
            context.Response.Body = originalResponseBody;

            _logger.LogInformation(responseContent.ToString());
        }
        private async Task LogRequest(HttpContext context)
        {
            var requestContent = new StringBuilder();

            requestContent.AppendLine($"method = {context.Request.Method.ToUpper()}");
            requestContent.AppendLine($"path = {context.Request.Path}");

            foreach(var(headerKey,headerValue) in context.Request.Headers)
            {
                requestContent.AppendLine($"header = {headerKey} value = {headerValue}");
            }
            requestContent.AppendLine("body");
            context.Request.EnableBuffering();
            var requestReader = new StreamReader(context.Request.Body);
            var content = await requestReader.ReadToEndAsync();
            requestContent.AppendLine($"body = {content}");
            _logger.LogInformation(requestContent.ToString());
            context.Request.Body.Position = 0;
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Middleware>();
        }
    }
}
