using System.Text;

namespace EnterpriceApp.API.Middleware
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

        public RequestResponseLoggingMiddleware(
            RequestDelegate next,
            ILogger<RequestResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var request = context.Request;

            // READ REQUEST BODY

            request.EnableBuffering();

            string requestBody = "";

            if (request.ContentLength > 0)
            {
                using var reader = new StreamReader(
                    request.Body,
                    Encoding.UTF8,
                    leaveOpen: true
                );

                requestBody = await reader.ReadToEndAsync();

                request.Body.Position = 0;
            }

            // CAPTURE RESPONSE

            var originalBodyStream = context.Response.Body;

            using var responseBody = new MemoryStream();

            context.Response.Body = responseBody;

            await _next(context);

            // READ RESPONSE

            context.Response.Body.Seek(0, SeekOrigin.Begin);

            string responseText = await new StreamReader(
                context.Response.Body
            ).ReadToEndAsync();

            context.Response.Body.Seek(0, SeekOrigin.Begin);

            // API NAME

            var apiName = $"{request.Method} {request.Path}";

            // LOG

            _logger.LogInformation(@"
                        ================ API REQUEST ================

                        API         : {ApiName}

                        RequestBody : {RequestBody}

                        StatusCode  : {StatusCode}

                        Response    : {Response}

                        =============================================
                        ",
                                        apiName,
                requestBody,
                context.Response.StatusCode,
                responseText
            );

            // COPY RESPONSE BACK

            await responseBody.CopyToAsync(originalBodyStream);
        }
    }
}
